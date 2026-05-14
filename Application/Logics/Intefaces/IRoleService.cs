using Application.DTOs.Roles;
using Application.DTOs.Users;

namespace Application.Logics.Intefaces
{
    public interface IRoleService
    {
        Task<RoleDto> CreateAsync(CreateRoleDto dto);
        Task<RoleDto> UpdateAsync(UpdateRoleDto dto);
        Task DeleteAsync(long id);
        Task<RoleDto> GetByIdAsync(long id);
        Task<RoleDto> GetByNameAsync(string name);
        Task<IReadOnlyList<RoleDto>> GetAllAsync();
        Task AssignRoleToUserAsync(long userId, long roleId);
        Task UnassignRoleFromUserAsync(long userId, long roleId);
        Task<IReadOnlyList<string>> GetUserRolesAsync(long userId);
        Task<IReadOnlyList<UserDto>> GetUsersInRoleAsync(long roleId);
    }
}