using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("tb_comments");

            builder.HasKey(c => c.Id);


            builder.Property(c => c.UserId).HasColumnName("user_id").IsRequired();

            builder.HasOne(c => c.User)
                   .WithMany(c => c.Comments)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.EventId).HasColumnName("event_id").IsRequired();

            builder.HasOne(c => c.Event)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(c => c.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Content).HasColumnName("content").IsRequired().HasMaxLength(255);

            //BaseEntity properties
            builder.Property(c => c.Id).HasColumnName("id").IsRequired();
            builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(c => c.CreatedUser).HasColumnName("created_user");
            builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
            builder.Property(c => c.UpdatedUser).HasColumnName("updated_user");
            builder.Property(c => c.DeletedAt).HasColumnName("deleted_at");
            builder.Property(c => c.DeletedUser).HasColumnName("deleted_user");
            builder.Property(c => c.Deleted).HasColumnName("deleted");
        }
    }
}
