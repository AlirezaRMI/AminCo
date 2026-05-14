using Application.DTOs.Common;
using Application.DTOs.PortfolioImages;
using Domain.Enums;

namespace Application.DTOs.Portfolios
{
    public class PortfolioDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? ClientName { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string? ProjectUrl { get; set; }
        public PortfolioCategory Category { get; set; }
        public int DisplayOrder { get; set; }
        public List<PortfolioImageDto> Images { get; set; } = new();
    }

    public class CreatePortfolioDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? ClientName { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string? ProjectUrl { get; set; }
        public PortfolioCategory Category { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class UpdatePortfolioDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public string? ClientName { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string? ProjectUrl { get; set; }
        public PortfolioCategory Category { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}