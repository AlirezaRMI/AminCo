using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services", "Content");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Title).HasMaxLength(200).IsRequired();
            builder.Property(s => s.Description).HasMaxLength(1000).IsRequired();
            builder.Property(s => s.IconUrl).HasMaxLength(500).IsRequired(false);
            builder.Property(s => s.DisplayOrder).HasDefaultValue(0);
            builder.Property(s => s.IsActive).HasDefaultValue(true);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);
        }
    }
}