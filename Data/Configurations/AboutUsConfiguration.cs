using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class AboutUsConfiguration : IEntityTypeConfiguration<AboutUs>
    {
        public void Configure(EntityTypeBuilder<AboutUs> builder)
        {
            builder.ToTable("AboutUs", "Content");

            builder.HasKey(au => au.Id);

            builder.HasData(new AboutUs
            {
                Id = 1,
                Title = "درباره ما",
                Content = "متن پیش‌فرض درباره ما - بعداً ویرایش شود",
                LastUpdated = DateTime.UtcNow
            });

            builder.Property(au => au.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(au => au.Content)
                .IsRequired();

            builder.Property(au => au.ImageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(au => au.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}