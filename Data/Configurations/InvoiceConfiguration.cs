using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices", "Sales");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(i => i.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(i => i.PdfUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(i => i.IsFinal)
                .HasDefaultValue(false);

            builder.Property(i => i.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(i => i.InvoiceNumber)
                .IsUnique()
                .HasDatabaseName("IX_Invoices_Number");

            builder.HasOne(i => i.Order)
                .WithOne(o => o.Invoice)
                .HasForeignKey<Invoice>(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}