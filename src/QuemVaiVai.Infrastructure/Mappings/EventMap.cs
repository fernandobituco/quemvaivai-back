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
    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("tb_events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title).HasColumnName("title").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.Location).HasColumnName("location").HasMaxLength(255);
            builder.Property(e => e.EventDate).HasColumnName("event_date");
            builder.Property(g => g.InviteCode).HasColumnName("invite_code").IsRequired();

            builder.Property(e => e.GroupId).HasColumnName("group_id");

            builder.HasOne(e => e.Group)
                   .WithMany(g => g.Events)
                   .HasForeignKey(e => e.GroupId);

            //BaseEntity properties
            builder.Property(e => e.Id).HasColumnName("id").IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(e => e.CreatedUser).HasColumnName("created_user");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.UpdatedUser).HasColumnName("updated_user");
            builder.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            builder.Property(e => e.DeletedUser).HasColumnName("deleted_user");
            builder.Property(e => e.Deleted).HasColumnName("deleted");
        }
    }
}
