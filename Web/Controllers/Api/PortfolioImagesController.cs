using Application.DTOs.PortfolioImages;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioImagesController(IPortfolioImageService imageService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ApiResult<PortfolioImageDto>> GetById(long id)
            => await imageService.GetByIdAsync(id);

        [HttpGet("by-portfolio/{portfolioId}")]
        public async Task<IReadOnlyList<PortfolioImageDto>> GetByPortfolioId(long portfolioId)
            => await imageService.GetByPortfolioIdAsync(portfolioId);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<PortfolioImageDto>> Create(CreatePortfolioImageDto dto)
            => await imageService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<PortfolioImageDto>> Update(UpdatePortfolioImageDto dto)
            => await imageService.UpdateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await imageService.DeleteAsync(id);
            return new OkResult();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("set-main")]
        public async Task<ApiResult> SetMainImage(long portfolioId, long imageId)
        {
            await imageService.SetMainImageAsync(portfolioId, imageId);
            return new OkResult();
        }
    }
}