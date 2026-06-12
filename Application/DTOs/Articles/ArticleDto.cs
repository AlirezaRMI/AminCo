using Application.DTOs.Common;

namespace Application.DTOs.Articles
{
    public class ArticleDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? MainImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public int ViewCount { get; set; }
        public bool IsPublished { get; set; }
    }

    public class CreateArticleDto
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        
        public bool IsPublished { get; set; }
        public string? MainImageUrl { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
    }

    public class UpdateArticleDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? MainImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public bool IsActive { get; set; }
    }
}