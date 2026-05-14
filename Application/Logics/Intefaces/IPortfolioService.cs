using Application.DTOs.Portfolios;
using Domain.Enums;

namespace Application.Logics.Intefaces
{
    public interface IPortfolioService
    {
        Task<PortfolioDto> CreateAsync(CreatePortfolioDto dto);
        Task<PortfolioDto> UpdateAsync(UpdatePortfolioDto dto);
        Task DeleteAsync(long id);
        Task<PortfolioDto> GetByIdAsync(long id);
        Task<IReadOnlyList<PortfolioDto>> GetAllAsync();
        Task<IReadOnlyList<PortfolioDto>> GetActivePortfoliosAsync();
        Task<IReadOnlyList<PortfolioDto>> GetByCategoryAsync(PortfolioCategory category);
    }
}