using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappinprts
{
    internal class PasswordResetTokenMap : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.ToTable("tb_password_reset_token");

            builder.HasKey(prt => prt.Id);

            builder.Property(prt => prt.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(prt => prt.ExpiresAt).HasColumnName("expires_at").IsRequired();
            builder.Property(prt => prt.TokenHash).HasColumnName("token_hash").IsRequired();
            builder.Property(prt => prt.Used).HasColumnName("used").IsRequired();

            builder.HasOne(prt => prt.User)
                   .WithMany(u => u.PasswordResetTokens)
                   .HasForeignKey(prt => prt.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //BaseEntity properties
            builder.Property(prt => prt.Id).HasColumnName("id").IsRequired();
            builder.Property(prt => prt.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(prt => prt.CreatedUser).HasColumnName("created_user");
            builder.Property(prt => prt.UpdatedAt).HasColumnName("updated_at");
            builder.Property(prt => prt.UpdatedUser).HasColumnName("updated_user");
            builder.Property(prt => prt.DeletedAt).HasColumnName("deleted_at");
            builder.Property(prt => prt.DeletedUser).HasColumnName("deleted_user");
            builder.Property(prt => prt.Deleted).HasColumnName("deleted");
        }
    }
}
