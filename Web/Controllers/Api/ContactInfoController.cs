using Application.DTOs.ContactInfo;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController(IContactInfoService contactService) : ControllerBase
    {
        [HttpGet]
        public async Task<ApiResult<ContactInfoDto>> Get()
            => await contactService.GetAsync();

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ApiResult<ContactInfoDto>> Update(UpdateContactInfoDto dto)
            => await contactService.UpdateAsync(dto);
    }
}