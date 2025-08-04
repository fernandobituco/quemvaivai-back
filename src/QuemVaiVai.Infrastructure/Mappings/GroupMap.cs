using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappings
{
    public class GroupMap : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("groups");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            builder.Property(g => g.Description).HasColumnName("description").IsRequired().HasMaxLength(100);

            //BaseEntity properties
            builder.Property(g => g.Id).HasColumnName("id").IsRequired();
            builder.Property(g => g.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(g => g.CreatedUser).HasColumnName("created_user").IsRequired();
            builder.Property(g => g.UpdatedAt).HasColumnName("updated_at");
            builder.Property(g => g.UpdatedUser).HasColumnName("updated_user");
            builder.Property(g => g.DeletedAt).HasColumnName("deleted_at");
            builder.Property(g => g.DeletedUser).HasColumnName("deleted_user");
            builder.Property(g => g.Deleted).HasColumnName("deleted");
        }
    }
}
