
using Microsoft.EntityFrameworkCore;
using QuemVaiVai.Domain.Entities;
using System.Linq.Expressions;

namespace QuemVaiVai.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
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
    }
}
