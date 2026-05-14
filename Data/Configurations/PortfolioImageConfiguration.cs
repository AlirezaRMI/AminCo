using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class PortfolioImageConfiguration : IEntityTypeConfiguration<PortfolioImage>
    {
        public void Configure(EntityTypeBuilder<PortfolioImage> builder)
        {
            builder.ToTable("PortfolioImages", "Content");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.ImageUrl)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(pi => pi.Title)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(pi => pi.AltText)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(pi => pi.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(pi => pi.IsMain)
                .HasDefaultValue(false);

            builder.Property(pi => pi.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(pi => pi.Portfolio)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}