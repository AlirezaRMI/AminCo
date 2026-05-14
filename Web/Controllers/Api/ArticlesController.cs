using Application.DTOs.Articles;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController(IArticleService articleService) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyList<ArticleDto>> GetAll([FromQuery] bool? onlyPublished)
            => await articleService.GetAllAsync(onlyPublished ?? true);

        [HttpGet("{id}")]
        public async Task<ApiResult<ArticleDto>> GetById(long id)
            => await articleService.GetByIdAsync(id);

        [HttpGet("slug/{slug}")]
        public async Task<ApiResult<ArticleDto>> GetBySlug(string slug)
            => await articleService.GetBySlugAsync(slug);

        [HttpGet("search")]
        public async Task<IReadOnlyList<ArticleDto>> Search([FromQuery] string? term)
            => await articleService.SearchAsync(term);

        [HttpPost("{id}/increment-view")]
        public async Task<ApiResult> IncrementViewCount(long id)
        {
            await articleService.IncrementViewCountAsync(id);
            return new OkResult();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<ArticleDto>> Create(CreateArticleDto dto)
            => await articleService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<ArticleDto>> Update(UpdateArticleDto dto)
            => await articleService.UpdateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await articleService.DeleteAsync(id);
            return new OkResult();
        }
    }
}