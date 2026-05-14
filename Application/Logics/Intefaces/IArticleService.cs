using Application.DTOs.Articles;

namespace Application.Logics.Intefaces
{
    public interface IArticleService
    {
        Task<ArticleDto> CreateAsync(CreateArticleDto dto);
        Task<ArticleDto> UpdateAsync(UpdateArticleDto dto);
        Task DeleteAsync(long id);
        Task<ArticleDto> GetByIdAsync(long id);
        Task<ArticleDto> GetBySlugAsync(string slug);
        Task<IReadOnlyList<ArticleDto>> GetAllAsync(bool onlyPublished = true);
        Task<IReadOnlyList<ArticleDto>> SearchAsync(string? searchTerm);
        Task IncrementViewCountAsync(long id);
    }
}