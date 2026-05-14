using Application.DTOs.Portfolios;
using Application.Logics.Intefaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController(IPortfolioService portfolioService) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyList<PortfolioDto>> GetAll()
            => await portfolioService.GetAllAsync();

        [HttpGet("active")]
        public async Task<IReadOnlyList<PortfolioDto>> GetActive()
            => await portfolioService.GetActivePortfoliosAsync();

        [HttpGet("category/{category}")]
        public async Task<IReadOnlyList<PortfolioDto>> GetByCategory(PortfolioCategory category)
            => await portfolioService.GetByCategoryAsync(category);

        [HttpGet("{id}")]
        public async Task<ApiResult<PortfolioDto>> GetById(long id)
            => await portfolioService.GetByIdAsync(id);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<PortfolioDto>> Create(CreatePortfolioDto dto)
            => await portfolioService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<PortfolioDto>> Update(UpdatePortfolioDto dto)
            => await portfolioService.UpdateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await portfolioService.DeleteAsync(id);
            return new OkResult();
        }
    }
}