using Domain.Entites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CustomDesignRequestConfiguration : IEntityTypeConfiguration<CustomDesignRequest>
    {
        public void Configure(EntityTypeBuilder<CustomDesignRequest> builder)
        {
            builder.ToTable("CustomDesignRequests", "Requests");

            builder.HasKey(cdr => cdr.Id);

            builder.Property(cdr => cdr.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(cdr => cdr.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(cdr => cdr.Phone)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(cdr => cdr.Description)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(cdr => cdr.Attachments)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(cdr => cdr.Status)
                .HasDefaultValue(RequestStatus.Pending);

            builder.Property(cdr => cdr.AdminResponse)
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(cdr => cdr.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(cdr => cdr.Email)
                .HasDatabaseName("IX_CustomDesignRequests_Email");

            builder.HasIndex(cdr => cdr.Status)
                .HasDatabaseName("IX_CustomDesignRequests_Status");
        }
    }
}