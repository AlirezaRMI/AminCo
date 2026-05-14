using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfo", "Content");

            builder.HasKey(ci => ci.Id);

            builder.HasData(new ContactInfo
            {
                Id = 1,
                Phone = "021-12345678",
                Email = "info@aminco.com",
                Address = "تهران، خیابان ولیعصر"
            });

            builder.Property(ci => ci.Phone)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(ci => ci.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ci => ci.Address)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(ci => ci.WorkingHours)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(ci => ci.GoogleMapUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(ci => ci.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}