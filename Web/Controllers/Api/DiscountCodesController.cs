using Application.DTOs.DiscountCodes;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCodesController(IDiscountCodeService discountService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IReadOnlyList<DiscountCodeDto>> GetAll()
            => await discountService.GetAllAsync();

        [HttpGet("active")]
        public async Task<IReadOnlyList<DiscountCodeDto>> GetActive()
            => await discountService.GetActiveCodesAsync();

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<DiscountCodeDto>> GetById(long id)
            => await discountService.GetByIdAsync(id);

        [HttpPost("validate")]
        public async Task<ApiResult<DiscountCodeDto?>> ValidateCode([FromBody] string code)
            => await discountService.ValidateCodeAsync(code);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResult<DiscountCodeDto>> Create(CreateDiscountCodeDto dto)
            => await discountService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<DiscountCodeDto>> Update(UpdateDiscountCodeDto dto)
            => await discountService.UpdateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await discountService.DeleteAsync(id);
            return new OkResult();
        }
    }
}