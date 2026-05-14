using Application.DTOs.DiscountCodes;

namespace Application.Logics.Intefaces
{
    public interface IDiscountCodeService
    {
        Task<DiscountCodeDto> CreateAsync(CreateDiscountCodeDto dto);
        Task<DiscountCodeDto> UpdateAsync(UpdateDiscountCodeDto dto);
        Task DeleteAsync(long id);
        Task<DiscountCodeDto> GetByIdAsync(long id);
        Task<IReadOnlyList<DiscountCodeDto>> GetAllAsync();
        Task<IReadOnlyList<DiscountCodeDto>> GetActiveCodesAsync();
        Task<DiscountCodeDto?> ValidateCodeAsync(string code, decimal? orderTotal = null);
        Task IncrementUsageAsync(long id);
    }
}