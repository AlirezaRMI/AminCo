using Application.DTOs.SpecialSales;

namespace Application.Logics.Intefaces
{
    public interface ISpecialSaleService
    {
        Task<SpecialSaleDto> CreateAsync(CreateSpecialSaleDto dto);
        Task<SpecialSaleDto> UpdateAsync(UpdateSpecialSaleDto dto);
        Task DeleteAsync(long id);
        Task<SpecialSaleDto> GetByIdAsync(long id);
        Task<IReadOnlyList<SpecialSaleDto>> GetAllAsync();
        Task<IReadOnlyList<SpecialSaleDto>> GetActiveSalesAsync();
        Task<IReadOnlyList<SpecialSaleDto>> GetByProductIdAsync(long productId);
        Task<SpecialSaleDto?> GetActiveSaleForProductAsync(long productId);
    }
}