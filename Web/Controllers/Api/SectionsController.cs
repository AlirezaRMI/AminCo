using Application.DTOs.Sections;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController(ISectionService sectionService) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyList<SectionDto>> GetAll()
            => await sectionService.GetAllAsync();

        [HttpGet("active")]
        public async Task<IReadOnlyList<SectionDto>> GetActive()
            => await sectionService.GetActiveSectionsAsync();

        [HttpGet("{id}")]
        public async Task<ApiResult<SectionDto>> GetById(long id)
            => await sectionService.GetByIdAsync(id);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<SectionDto>> Create(CreateSectionDto dto)
            => await sectionService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<SectionDto>> Update(UpdateSectionDto dto)
            => await sectionService.UpdateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await sectionService.DeleteAsync(id);
            return new OkResult();
        }
    }
}