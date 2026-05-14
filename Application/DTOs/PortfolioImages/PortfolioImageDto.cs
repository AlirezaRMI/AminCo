using Application.DTOs.Common;

namespace Application.DTOs.PortfolioImages
{
    public class PortfolioImageDto : BaseDto
    {
        public long PortfolioId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? AltText { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMain { get; set; }
    }

    public class CreatePortfolioImageDto
    {
        public long PortfolioId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? AltText { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMain { get; set; }
    }

    public class UpdatePortfolioImageDto
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? AltText { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMain { get; set; }
    }
}