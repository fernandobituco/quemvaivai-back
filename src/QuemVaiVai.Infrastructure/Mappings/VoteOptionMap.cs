using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappings
{
    public class VoteOptionMap : IEntityTypeConfiguration<VoteOption>
    {
        public void Configure(EntityTypeBuilder<VoteOption> builder)
        {
            builder.ToTable("tb_vote_options");

            builder.HasKey(vo => vo.Id);


            builder.Property(vo => vo.SuggestedDate).HasColumnName("suggested_date");
            builder.Property(vo => vo.SuggestedLocation).HasColumnName("suggested_location");
            builder.Property(vo => vo.EventId).HasColumnName("event_id").IsRequired();

            //BaseEntity properties
            builder.Property(vo => vo.Id).HasColumnName("id").IsRequired();
            builder.Property(vo => vo.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(vo => vo.CreatedUser).HasColumnName("created_user");
            builder.Property(vo => vo.UpdatedAt).HasColumnName("updated_at");
            builder.Property(vo => vo.UpdatedUser).HasColumnName("updated_user");
            builder.Property(vo => vo.DeletedAt).HasColumnName("deleted_at");
            builder.Property(vo => vo.DeletedUser).HasColumnName("deleted_user");
            builder.Property(vo => vo.Deleted).HasColumnName("deleted");
        }
    }
}
