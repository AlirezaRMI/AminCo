using Application.DTOs.SpecialSales;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialSalesController(ISpecialSaleService saleService) : ControllerBase
    {

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IReadOnlyList<SpecialSaleDto>> GetAll()
            => await saleService.GetAllAsync();

        [HttpGet("active")]
        public async Task<IReadOnlyList<SpecialSaleDto>> GetActive()
            => await saleService.GetActiveSalesAsync();

        [HttpGet("{id}")]
        public async Task<ApiResult<SpecialSaleDto>> GetById(long id)
            => await saleService.GetByIdAsync(id);

        [HttpGet("by-product/{productId}")]
        public async Task<IReadOnlyList<SpecialSaleDto>> GetByProductId(long productId)
            => await saleService.GetByProductIdAsync(productId);

        [HttpGet("active-by-product/{productId}")]
        public async Task<ApiResult<SpecialSaleDto?>> GetActiveSaleForProduct(long productId)
            => await saleService.GetActiveSaleForProductAsync(productId);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<SpecialSaleDto>> Create(CreateSpecialSaleDto dto)
            => await saleService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<SpecialSaleDto>> Update(UpdateSpecialSaleDto dto)
            => await saleService.UpdateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await saleService.DeleteAsync(id);
            return new OkResult();
        }
    }
}