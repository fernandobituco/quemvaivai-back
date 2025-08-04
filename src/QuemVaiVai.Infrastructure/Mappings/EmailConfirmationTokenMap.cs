using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Infrastructure.Mappings
{
    public class EmailConfirmationTokenMap : IEntityTypeConfiguration<EmailConfirmationToken>
    {
        public void Configure(EntityTypeBuilder<EmailConfirmationToken> builder)
        {
            builder.ToTable("email_confirmation_tokens");

            builder.HasKey(ect => ect.Id);

            builder.Property(ect => ect.UserId).IsRequired().HasColumnName("user_id").HasMaxLength(100);
            builder.Property(ect => ect.Token).IsRequired().HasColumnName("token").HasMaxLength(100);
            builder.Property(ect => ect.Expiration).HasColumnName("expiration").IsRequired();
            builder.Property(ect => ect.Used).HasColumnName("used").IsRequired();

            //BaseEntity properties
            builder.Property(ect => ect.Id).HasColumnName("id").IsRequired();
            builder.Property(ect => ect.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(ect => ect.CreatedUser).HasColumnName("created_user").IsRequired();
            builder.Property(ect => ect.UpdatedAt).HasColumnName("updated_at");
            builder.Property(ect => ect.UpdatedUser).HasColumnName("updated_user");
            builder.Property(ect => ect.DeletedAt).HasColumnName("deleted_at");
            builder.Property(ect => ect.DeletedUser).HasColumnName("deleted_user");
            builder.Property(ect => ect.Deleted).HasColumnName("deleted");
        }
    }
}
