using Application.DTOs.Users;
using Application.Logics.Intefaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService, IRoleService roleService, IJwtService jwtService)
        : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ApiResult<UserDto>> Register(RegisterUserDto dto)
        {
            var user = await userService.RegisterAsync(dto);
            var customerRole = await roleService.GetByNameAsync("Customer");
            if (customerRole != null)
                await roleService.AssignRoleToUserAsync(user.Id, customerRole.Id);
            return user;
        }

        [HttpPost("login")]
        public async Task<ApiResult<LoginResponseDto>> Login(LoginUserDto dto)
        {
            var isValid = await userService.ValidateCredentialsAsync(dto.Email, dto.Password);
            if (!isValid)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است.");

            var user = await userService.GetByEmailAsync(dto.Email);
            var roles = await roleService.GetUserRolesAsync(user.Id);
            var token = jwtService.GenerateToken(user, roles);

            HttpContext.Response.Cookies.Append("accessToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(120)
            });

            return new LoginResponseDto(token, user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("accessToken");
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            var userId = User.GetUserId();
            var user = await userService.GetByIdAsync(userId);
            return user;
        }
    }

    public record LoginResponseDto(string Token, UserDto User);
}