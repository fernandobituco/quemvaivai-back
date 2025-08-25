using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappings
{
    public class VoteMap : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("tb_votes");

            builder.HasKey(v => v.Id);


            builder.Property(v => v.VoteOptionId).HasColumnName("vote_option_id").IsRequired();

            builder.HasOne(ue => ue.VoteOption)
                   .WithMany(e => e.Votes)
                   .HasForeignKey(ue => ue.VoteOptionId);

            builder.Property(v => v.UserId).HasColumnName("user_id").IsRequired();

            builder.HasOne(ue => ue.User)
                   .WithMany(e => e.Votes)
                   .HasForeignKey(ue => ue.UserId);

            //BaseEntity properties
            builder.Property(v => v.Id).HasColumnName("id").IsRequired();
            builder.Property(v => v.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(v => v.CreatedUser).HasColumnName("created_user");
            builder.Property(v => v.UpdatedAt).HasColumnName("updated_at");
            builder.Property(v => v.UpdatedUser).HasColumnName("updated_user");
            builder.Property(v => v.DeletedAt).HasColumnName("deleted_at");
            builder.Property(v => v.DeletedUser).HasColumnName("deleted_user");
            builder.Property(v => v.Deleted).HasColumnName("deleted");
        }
    }
}
