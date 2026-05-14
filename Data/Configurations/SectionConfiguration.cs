using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("Sections", "Commerce");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(s => s.Icon)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(s => s.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);

            builder.Property(s => s.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(s => s.Name)
                .HasDatabaseName("IX_Sections_Name");

            builder.HasMany(s => s.Categories)
                .WithOne(c => c.Section)
                .HasForeignKey(c => c.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}