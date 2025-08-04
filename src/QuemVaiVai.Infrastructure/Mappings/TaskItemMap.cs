using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappings
{
    internal class TaskItemMap : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("task_items");

            builder.HasKey(ti => ti.Id);


            builder.Property(ti => ti.Description).HasColumnName("description").IsRequired();
            builder.Property(ti => ti.IsDone).HasColumnName("is_done").IsRequired();

            builder.HasOne(ti => ti.TaskList)
                   .WithMany(tl => tl.TaskItems)
                   .HasForeignKey(ti => ti.TaskListId)
                   .OnDelete(DeleteBehavior.Cascade);

            //BaseEntity properties
            builder.Property(ti => ti.Id).HasColumnName("id").IsRequired();
            builder.Property(ti => ti.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(ti => ti.CreatedUser).HasColumnName("created_user").IsRequired();
            builder.Property(ti => ti.UpdatedAt).HasColumnName("updated_at");
            builder.Property(ti => ti.UpdatedUser).HasColumnName("updated_user");
            builder.Property(ti => ti.DeletedAt).HasColumnName("deleted_at");
            builder.Property(ti => ti.DeletedUser).HasColumnName("deleted_user");
            builder.Property(ti => ti.Deleted).HasColumnName("deleted");
        }
    }
}
