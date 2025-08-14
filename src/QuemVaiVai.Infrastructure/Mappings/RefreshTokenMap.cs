using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappings
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.HasKey(ue => ue.Id);

            builder.Property(u => u.UserId).HasColumnName("user_id").IsRequired();

            builder.Property(ue => ue.Id).HasColumnName("id").IsRequired();
            builder.Property(ue => ue.Token).HasColumnName("token").IsRequired();
            builder.Property(ue => ue.ExpiryDate).HasColumnName("expiry_date").IsRequired();
            builder.Property(ue => ue.CreatedDate).HasColumnName("created_date").IsRequired();
            builder.Property(ue => ue.IsRevoked).HasColumnName("is_revoked").IsRequired();
            builder.Property(ue => ue.ReplacedByToken).HasColumnName("replaced_by_token");
        }
    }
}
