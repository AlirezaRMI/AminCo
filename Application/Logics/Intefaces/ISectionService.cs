using Application.DTOs.Sections;

namespace Application.Logics.Intefaces
{
    public interface ISectionService
    {
        Task<SectionDto> CreateAsync(CreateSectionDto dto);
        Task<SectionDto> UpdateAsync(UpdateSectionDto dto);
        Task DeleteAsync(long id);
        Task<SectionDto> GetByIdAsync(long id);
        Task<IReadOnlyList<SectionDto>> GetAllAsync();
        Task<IReadOnlyList<SectionDto>> GetActiveSectionsAsync();
        Task<IReadOnlyList<SectionDto>> SearchAsync(string? searchTerm);
    }
}