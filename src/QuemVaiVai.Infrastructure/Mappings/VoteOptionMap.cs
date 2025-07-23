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
    public class VoteOptionMap : IEntityTypeConfiguration<VoteOption>
    {
        public void Configure(EntityTypeBuilder<VoteOption> builder)
        {
            builder.ToTable("vote_options");

            builder.HasKey(vo => vo.Id);

            builder.Property(vo => vo.Id).HasColumnName("id").IsRequired();

            builder.Property(vo => vo.SuggestedDate).HasColumnName("suggested_date");
            builder.Property(vo => vo.SuggestedLocation).HasColumnName("suggested_location");
            builder.Property(vo => vo.EventId).HasColumnName("event_id").IsRequired();

            //BaseEntity properties
            builder.Property(vo => vo.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(vo => vo.CreatedUser).HasColumnName("created_user").IsRequired();
            builder.Property(vo => vo.UpdatedAt).HasColumnName("updated_at");
            builder.Property(vo => vo.UpdatedUser).HasColumnName("updated_user");
            builder.Property(vo => vo.DeletedAt).HasColumnName("deleted_at");
            builder.Property(vo => vo.DeletedUser).HasColumnName("deleted_user");
            builder.Property(c => c.Deleted).HasColumnName("deleted");
        }
    }
}
