
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Domain.Entities;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace QuemVaiVai.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        private readonly IUserContext? _userContext;
        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IUserContext? userContext = null) : base(options)
        {
            _userContext = userContext;
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<GroupUser> GroupUsers => Set<GroupUser>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<UserEvent> UserEvents => Set<UserEvent>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Vote> Votes => Set<Vote>();
        public DbSet<VoteOption> VoteOptions => Set<VoteOption>();
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();
        public DbSet<TaskList> TaskLists => Set<TaskList>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                // Configuração de geração automática de ID
                var idProp = clrType.GetProperty("Id");
                if (idProp != null && idProp.PropertyType == typeof(int))
                {
                    modelBuilder.Entity(clrType)
                        .Property("Id")
                        .ValueGeneratedOnAdd();
                }

                // Filtro para deleção lógica
                var deletedProp = clrType.GetProperty("Deleted");

                if (deletedProp != null && deletedProp.PropertyType == typeof(bool))
                {
                    var parameter = Expression.Parameter(clrType, "p");
                    var propAccess = Expression.MakeMemberAccess(parameter, deletedProp);
                    var notDeleted = Expression.Not(propAccess);
                    var lambda = Expression.Lambda(notDeleted, parameter);

                    modelBuilder.Entity(clrType).HasQueryFilter(lambda);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            TrackEntityChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackEntityChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void TrackEntityChanges()
        {
            var userId = _userContext?.GetCurrentUserId() ?? 0;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added))
            {
                if (entry.Entity.CreatedAt == default)
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                if (entry.Entity.CreatedUser == 0)
                    entry.Entity.CreatedUser = userId;
            }

            foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Modified))
            {
                if (entry.Entity.UpdatedAt == default)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                if (entry.Entity.UpdatedUser == 0)
                    entry.Entity.UpdatedUser = userId;
            }

            foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified; // Alterar o estado para modificado para aplicar deleção lógica
                entry.Entity.Deleted = true;
                if (entry.Entity.DeletedAt == default)
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                if (entry.Entity.DeletedUser == 0)
                    entry.Entity.DeletedUser = userId;
            }
        }
    }
}
