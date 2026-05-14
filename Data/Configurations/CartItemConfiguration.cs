using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems", "Sales");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Quantity)
                .HasDefaultValue(1);

            builder.Property(ci => ci.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(ci => ci.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}