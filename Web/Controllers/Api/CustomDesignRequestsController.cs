using Application.DTOs.CustomDesignRequests;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomDesignRequestsController(ICustomDesignRequestService requestService) : ControllerBase
    {
        [HttpPost]
        public async Task<ApiResult<CustomDesignRequestDto>> Create(CreateCustomDesignRequestDto dto)
            => await requestService.CreateAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IReadOnlyList<CustomDesignRequestDto>> GetAll([FromQuery] bool? onlyPending)
            => await requestService.GetAllAsync(onlyPending ?? false);

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ApiResult<CustomDesignRequestDto>> GetById(long id)
            => await requestService.GetByIdAsync(id);

        [Authorize(Roles = "Admin")]
        [HttpPut("status")]
        public async Task<ApiResult<CustomDesignRequestDto>> UpdateStatus(UpdateCustomDesignRequestDto dto)
            => await requestService.UpdateStatusAsync(dto);

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await requestService.DeleteAsync(id);
            return new OkResult();
        }
    }
}