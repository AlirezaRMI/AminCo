using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DiscountCodeConfiguration : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> builder)
        {
            builder.ToTable("DiscountCodes", "Commerce");

            builder.HasKey(dc => dc.Id);

            builder.Property(dc => dc.Code)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(dc => dc.Value)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(dc => dc.UsageLimit)
                .HasDefaultValue(1);

            builder.Property(dc => dc.UsedCount)
                .HasDefaultValue(0);

            builder.Property(dc => dc.IsActive)
                .HasDefaultValue(true);

            builder.Property(dc => dc.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(dc => dc.Code)
                .IsUnique()
                .HasDatabaseName("IX_DiscountCodes_Code");
        }
    }
}