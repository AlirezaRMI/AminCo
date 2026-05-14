using Application.DTOs.AboutUs;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutUsController(IAboutUsService aboutService) : ControllerBase
    {
        [HttpGet]
        public async Task<ApiResult<AboutUsDto>> Get()
            => await aboutService.GetAsync();

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<AboutUsDto>> Update(UpdateAboutUsDto dto)
            => await aboutService.UpdateAsync(dto);
    }
}