using Domain.Entites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "Sales");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.SubTotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(o => o.DiscountAmount)
                .HasPrecision(18, 2)
                .HasDefaultValue(0);

            builder.Property(o => o.TaxAmount)
                .HasPrecision(18, 2)
                .HasDefaultValue(0);

            builder.Property(o => o.TotalAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(o => o.DiscountCode)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(o => o.ShippingAddress)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(o => o.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Pending);

            builder.Property(o => o.IsPaid)
                .HasDefaultValue(false);

            builder.Property(o => o.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(o => o.UserId)
                .HasDatabaseName("IX_Orders_UserId");

            builder.HasIndex(o => o.OrderDate)
                .HasDatabaseName("IX_Orders_OrderDate");

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Invoice)
                .WithOne(i => i.Order)
                .HasForeignKey<Invoice>(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}