using Application.DTOs.Categories;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyList<CategoryDto>> GetAll()
            => await categoryService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ApiResult<CategoryDto>> GetById(long id)
            => await categoryService.GetByIdAsync(id);

        [HttpGet("by-section/{sectionId}")]
        public async Task<IReadOnlyList<CategoryDto>> GetBySectionId(long sectionId)
            => await categoryService.GetBySectionIdAsync(sectionId);

        [HttpGet("search")]
        public async Task<IReadOnlyList<CategoryDto>> Search([FromQuery] string? term)
            => await categoryService.SearchAsync(term);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<CategoryDto>> Create(CreateCategoryDto dto)
            => await categoryService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<CategoryDto>> Update(UpdateCategoryDto dto)
            => await categoryService.UpdateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await categoryService.DeleteAsync(id);
            return new OkResult();
        }
    }
}