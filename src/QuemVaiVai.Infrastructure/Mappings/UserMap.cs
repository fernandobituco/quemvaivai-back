using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("id").IsRequired();

            builder.Property(u => u.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasColumnName("email").HasMaxLength(100);
            builder.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired();

            //BaseEntity properties
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.CreatedUser).HasColumnName("created_user").IsRequired();
            builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");
            builder.Property(u => u.UpdatedUser).HasColumnName("updated_user");
            builder.Property(u => u.DeletedAt).HasColumnName("deleted_at");
            builder.Property(u => u.DeletedUser).HasColumnName("deleted_user");
            builder.Property(c => c.Deleted).HasColumnName("deleted");
        }
    }
}
