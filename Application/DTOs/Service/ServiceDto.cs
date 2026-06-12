// Application/DTOs/Services/ServiceDto.cs
namespace Application.DTOs.Service
{
    public class ServiceDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}