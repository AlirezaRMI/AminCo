using Application.DTOs.Categories;

namespace Application.Logics.Intefaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto);
        Task DeleteAsync(long id);
        Task<CategoryDto> GetByIdAsync(long id);
        Task<IReadOnlyList<CategoryDto>> GetAllAsync();
        Task<IReadOnlyList<CategoryDto>> GetBySectionIdAsync(long sectionId);
        Task<IReadOnlyList<CategoryDto>> SearchAsync(string? searchTerm);
    }
}