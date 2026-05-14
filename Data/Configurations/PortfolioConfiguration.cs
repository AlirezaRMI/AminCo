using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.ToTable("Portfolios", "Content");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Slug)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(p => p.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(p => p.ClientName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(p => p.ProjectUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(p => p.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(p => p.Title)
                .HasDatabaseName("IX_Portfolios_Title");

            builder.HasMany(p => p.Images)
                .WithOne(i => i.Portfolio)
                .HasForeignKey(i => i.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}