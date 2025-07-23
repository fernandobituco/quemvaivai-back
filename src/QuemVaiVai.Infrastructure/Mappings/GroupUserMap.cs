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
    public class GroupUserMap : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.ToTable("group_users");

            builder.HasKey(gu => gu.Id);

            builder.Property(gu => gu.Id).HasColumnName("id").IsRequired();

            builder.Property(gu => gu.UserId).HasColumnName("user_id").IsRequired();

            builder.HasOne(gu => gu.User)
                   .WithMany(u => u.GroupUsers)
                   .HasForeignKey(gu => gu.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.GroupId).HasColumnName("group_id").IsRequired();

            builder.HasOne(gu => gu.Group)
                   .WithMany(g => g.GroupUsers)
                   .HasForeignKey(gu => gu.GroupId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(gu => gu.Role).HasColumnName("role").IsRequired(); // int ENUM

            //BaseEntity properties
            builder.Property(gu => gu.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(gu => gu.CreatedUser).HasColumnName("created_user").IsRequired();
            builder.Property(gu => gu.UpdatedAt).HasColumnName("updated_at");
            builder.Property(gu => gu.UpdatedUser).HasColumnName("updated_user");
            builder.Property(gu => gu.DeletedAt).HasColumnName("deleted_at");
            builder.Property(gu => gu.DeletedUser).HasColumnName("deleted_user");
            builder.Property(c => c.Deleted).HasColumnName("deleted");
        }
    }
}
