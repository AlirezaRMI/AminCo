using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "Commerce");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(c => c.Name)
                .HasDatabaseName("IX_Categories_Name");

            builder.HasOne(c => c.Section)
                .WithMany(s => s.Categories)
                .HasForeignKey(c => c.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}