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
    public class UserEventMap : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.ToTable("user_events");

            builder.HasKey(ue => ue.Id);

            builder.Property(ue => ue.Id).HasColumnName("id").IsRequired();

            builder.Property(u => u.UserId).HasColumnName("user_id").IsRequired();

            builder.HasOne(ue => ue.User)
                   .WithMany(u => u.UserEvents)
                   .HasForeignKey(ue => ue.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.EventId).HasColumnName("event_id").IsRequired();

            builder.HasOne(ue => ue.Event)
                   .WithMany(e => e.UserEvents)
                   .HasForeignKey(ue => ue.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ue => ue.Status).HasColumnName("status").IsRequired(); // int ENUM

            //BaseEntity properties
            builder.Property(ue => ue.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(ue => ue.CreatedUser).HasColumnName("created_user").IsRequired();
            builder.Property(ue => ue.UpdatedAt).HasColumnName("updated_at");
            builder.Property(ue => ue.UpdatedUser).HasColumnName("updated_user");
            builder.Property(ue => ue.DeletedAt).HasColumnName("deleted_at");
            builder.Property(ue => ue.DeletedUser).HasColumnName("deleted_user");
            builder.Property(c => c.Deleted).HasColumnName("deleted");
        }
    }
}
