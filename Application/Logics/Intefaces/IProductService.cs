using Application.DTOs.Products;

namespace Application.Logics.Intefaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto> UpdateAsync(UpdateProductDto dto);
        Task DeleteAsync(long id);
        Task<ProductDto> GetByIdAsync(long id);
        Task<IReadOnlyList<ProductDto>> GetAllAsync();
        Task<IReadOnlyList<ProductDto>> GetByCategoryIdAsync(long categoryId);
        Task<IReadOnlyList<ProductDto>> GetActiveProductsAsync();
        Task<IReadOnlyList<ProductDto>> SearchAsync(string? searchTerm);
        Task<bool> CheckStockAsync(long productId, int quantity);
        Task<decimal> GetEffectivePriceAsync(long productId);
    }
}