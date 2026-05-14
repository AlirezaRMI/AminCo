using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class SpecialSaleConfiguration : IEntityTypeConfiguration<SpecialSale>
    {
        public void Configure(EntityTypeBuilder<SpecialSale> builder)
        {
            builder.ToTable("SpecialSales", "Commerce");

            builder.HasKey(ss => ss.Id);

            builder.Property(ss => ss.SalePrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(ss => ss.StartDate)
                .IsRequired();

            builder.Property(ss => ss.EndDate)
                .IsRequired();

            builder.Property(ss => ss.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(ss => new { ss.ProductId, ss.StartDate, ss.EndDate })
                .HasDatabaseName("IX_SpecialSales_Product_DateRange");

            builder.HasOne(ss => ss.Product)
                .WithMany(p => p.SpecialSales)
                .HasForeignKey(ss => ss.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}