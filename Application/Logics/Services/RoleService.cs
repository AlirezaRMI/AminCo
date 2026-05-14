using Application.DTOs.Roles;
using Application.DTOs.Users;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class RoleService(
        IAsyncRepository<Role, long> roleRepo,
        IAsyncRepository<UserRole, long> userRoleRepo,
        IAsyncRepository<User, long> userRepo,
        IMapper mapper,
        ILogger<RoleService> logger)
        : IRoleService
    {
        public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
        {
            var existing = await roleRepo.GetSingleAsync(r => r.Name == dto.Name);
            if (existing != null)
                throw new ExistsException("نقش با این نام قبلاً ثبت شده است.");
            var role = new Role {Name = dto.Name, IsActive = true};
            await roleRepo.AddEntity(role);
            await roleRepo.SaveChangesAsync();
            return mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> UpdateAsync(UpdateRoleDto dto)
        {
            var role = await roleRepo.GetByIdAsync(dto.Id);
            if (role == null)
                throw new NotFoundException("نقش یافت نشد.");
            role.Name = dto.Name;
            role.IsActive = dto.IsActive;
            await roleRepo.UpdateEntity(role);
            await roleRepo.SaveChangesAsync();
            return mapper.Map<RoleDto>(role);
        }

        public async Task DeleteAsync(long id)
        {
            var role = await roleRepo.GetByIdAsync(id);
            if (role == null)
                throw new NotFoundException("نقش یافت نشد.");
            role.IsDeleted = true;
            role.IsActive = false;
            await roleRepo.UpdateEntity(role);
            await roleRepo.SaveChangesAsync();
        }

        public async Task<RoleDto> GetByIdAsync(long id)
        {
            var role = await roleRepo.GetSingleAsync(r => r.Id == id && !r.IsDeleted);
            if (role == null)
                throw new NotFoundException("نقش یافت نشد.");
            return mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> GetByNameAsync(string name)
        {
            var role = await roleRepo.GetSingleAsync(r => r.Name == name && !r.IsDeleted);
            return role == null ? null : mapper.Map<RoleDto>(role);
        }

        public async Task<IReadOnlyList<RoleDto>> GetAllAsync()
        {
            var roles = await roleRepo.GetAsync(r => !r.IsDeleted);
            return mapper.Map<IReadOnlyList<RoleDto>>(roles);
        }

        public async Task AssignRoleToUserAsync(long userId, long roleId)
        {
            var user = await userRepo.GetByIdAsync(userId);
            if (user == null) throw new NotFoundException("کاربر یافت نشد.");
            var role = await roleRepo.GetByIdAsync(roleId);
            if (role == null) throw new NotFoundException("نقش یافت نشد.");

            var exists = await userRoleRepo.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (exists) return;

            var userRole = new UserRole {UserId = userId, RoleId = roleId};
            await userRoleRepo.AddEntity(userRole);
            await userRoleRepo.SaveChangesAsync();
        }

        public async Task UnassignRoleFromUserAsync(long userId, long roleId)
        {
            var userRole = await userRoleRepo.GetSingleAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (userRole != null)
            {
                userRoleRepo.DeleteEntity(userRole);
                await userRoleRepo.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<string>> GetUserRolesAsync(long userId)
        {
            var userRoles = await userRoleRepo.GetAsync(ur => ur.UserId == userId, includes: [x => x.Role]);
            return userRoles.Select(ur => ur.Role!.Name).ToList();
        }

        public async Task<IReadOnlyList<UserDto>> GetUsersInRoleAsync(long roleId)
        {
            var userRoles = await userRoleRepo.GetAsync(ur => ur.RoleId == roleId, includes: [x => x.User]);
            var users = userRoles.Select(ur => ur.User).Where(u => u != null && !u.IsDeleted).ToList();
            return mapper.Map<IReadOnlyList<UserDto>>(users);
        }
    }
}