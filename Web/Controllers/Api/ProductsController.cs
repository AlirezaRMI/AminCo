using Application.DTOs.Products;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyList<ProductDto>> GetAll()
            => await productService.GetAllAsync();

        [HttpGet("active")]
        public async Task<IReadOnlyList<ProductDto>> GetActive()
            => await productService.GetActiveProductsAsync();

        [HttpGet("{id}")]
        public async Task<ApiResult<ProductDto>> GetById(long id)
            => await productService.GetByIdAsync(id);

        [HttpGet("by-category/{categoryId}")]
        public async Task<IReadOnlyList<ProductDto>> GetByCategoryId(long categoryId)
            => await productService.GetByCategoryIdAsync(categoryId);

        [HttpGet("search")]
        public async Task<IReadOnlyList<ProductDto>> Search([FromQuery] string? term)
            => await productService.SearchAsync(term);

        [HttpGet("effective-price/{id}")]
        public async Task<decimal> GetEffectivePrice(long id)
            => await productService.GetEffectivePriceAsync(id);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<ProductDto>> Create(CreateProductDto dto)
            => await productService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<ProductDto>> Update(UpdateProductDto dto)
            => await productService.UpdateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await productService.DeleteAsync(id);
            return new OkResult();
        }
    }
}