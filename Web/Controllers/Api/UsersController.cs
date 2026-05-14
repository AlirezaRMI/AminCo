using Application.DTOs.Users;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ApiResult<IReadOnlyList<UserDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<UserDto>> GetById(long id)
            => await userService.GetByIdAsync(id);

        [HttpPut]
        public async Task<ApiResult<UserDto>> Update(UpdateUserDto dto)
            => await userService.UpdateAsync(dto);

        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await userService.DeleteAsync(id);
            return new OkResult();
        }

        [HttpPost("change-password")]
        public async Task<ApiResult> ChangePassword(long userId, [FromBody] string newPassword)
        {
            await userService.ChangePasswordAsync(userId, newPassword);
            return new OkResult();
        }
    }
}