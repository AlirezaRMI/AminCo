using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles", "Content");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.Slug)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.ShortDescription)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(a => a.Content)
                .IsRequired();

            builder.Property(a => a.MainImageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(a => a.ViewCount)
                .HasDefaultValue(0);

            builder.Property(a => a.IsPublished)
                .HasDefaultValue(true);

            builder.Property(a => a.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(a => a.Slug)
                .IsUnique()
                .HasDatabaseName("IX_Articles_Slug");

            builder.HasIndex(a => a.PublishDate)
                .HasDatabaseName("IX_Articles_PublishDate");
        }
    }
}