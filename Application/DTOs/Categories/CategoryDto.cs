using Application.DTOs.Common;

namespace Application.DTOs.Categories
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public long SectionId { get; set; }
        public string? SectionName { get; set; }
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public long SectionId { get; set; }
    }

    public class UpdateCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public long SectionId { get; set; }
        public bool IsActive { get; set; }
    }
}