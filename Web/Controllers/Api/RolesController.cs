using Application.DTOs.Roles;
using Application.Logics.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyList<RoleDto>> GetAll()
            => await roleService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ApiResult<RoleDto>> GetById(long id)
            => await roleService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ApiResult<RoleDto>> Create(CreateRoleDto dto)
            => await roleService.CreateAsync(dto);

        [HttpPut]
        public async Task<ApiResult<RoleDto>> Update(UpdateRoleDto dto)
            => await roleService.UpdateAsync(dto);

        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            await roleService.DeleteAsync(id);
            return new OkResult();
        }

        [HttpPost("assign-role")]
        public async Task<ApiResult> AssignRoleToUser(long userId, long roleId)
        {
            await roleService.AssignRoleToUserAsync(userId, roleId);
            return new OkResult();
        }

        [HttpPost("unassign-role")]
        public async Task<ApiResult> UnassignRoleFromUser(long userId, long roleId)
        {
            await roleService.UnassignRoleFromUserAsync(userId, roleId);
            return new OkResult();
        }

        [HttpGet("user-roles/{userId}")]
        public async Task<IReadOnlyList<string>> GetUserRoles(long userId)
            => await roleService.GetUserRolesAsync(userId);
    }
}