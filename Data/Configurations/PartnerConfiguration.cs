using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.ToTable("Partners", "Content");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            builder.Property(p => p.LogoUrl).HasMaxLength(500).IsRequired(false);
            builder.Property(p => p.Website).HasMaxLength(300).IsRequired(false);
            builder.Property(p => p.DisplayOrder).HasDefaultValue(0);
            builder.Property(p => p.IsActive).HasDefaultValue(true);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}