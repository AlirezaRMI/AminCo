using Application.DTOs.PortfolioImages;

namespace Application.Logics.Intefaces
{
    public interface IPortfolioImageService
    {
        Task<PortfolioImageDto> CreateAsync(CreatePortfolioImageDto dto);
        Task<PortfolioImageDto> UpdateAsync(UpdatePortfolioImageDto dto);
        Task DeleteAsync(long id);
        Task<PortfolioImageDto> GetByIdAsync(long id);
        Task<IReadOnlyList<PortfolioImageDto>> GetByPortfolioIdAsync(long portfolioId);
        Task SetMainImageAsync(long portfolioId, long imageId);
    }
}