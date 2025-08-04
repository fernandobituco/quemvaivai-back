using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappings
{
    internal class TaskListMap : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.ToTable("task_lists");

            builder.HasKey(tl => tl.Id);


            builder.Property(tl => tl.Title).HasColumnName("title");

            builder.Property(tl => tl.EventId).HasColumnName("event_id");

            //BaseEntity properties
            builder.Property(tl => tl.Id).HasColumnName("id").IsRequired();
            builder.Property(tl => tl.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(tl => tl.CreatedUser).HasColumnName("created_user").IsRequired();
            builder.Property(tl => tl.UpdatedAt).HasColumnName("updated_at");
            builder.Property(tl => tl.UpdatedUser).HasColumnName("updated_user");
            builder.Property(tl => tl.DeletedAt).HasColumnName("deleted_at");
            builder.Property(tl => tl.DeletedUser).HasColumnName("deleted_user");
            builder.Property(tl => tl.Deleted).HasColumnName("deleted");
        }
    }
}
