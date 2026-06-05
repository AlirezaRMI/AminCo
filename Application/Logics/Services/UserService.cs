using System.Security.Cryptography;
using System.Text;
using Application.DTOs.Users;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class UserService(IAsyncRepository<User, long> repo, IMapper mapper, ILogger<UserService> logger)
        : IUserService
    {
        public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
        {
            var existing = await repo.GetSingleAsync(u => u.Email == dto.Email);
            if (existing != null) throw new BadRequestException("ایمیل تکراری است.");
            var user = mapper.Map<User>(dto);
            user.PasswordHash = HashPassword(dto.Password);
            user.IsActive = true;
            await repo.AddEntity(user);
            await repo.SaveChangesAsync();
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByIdAsync(long id)
        {
            var user = await repo.GetSingleAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) throw new NotFoundException("کاربر یافت نشد.");
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await repo.GetSingleAsync(u => u.Email == email && !u.IsDeleted);
            if (user == null) throw new NotFoundException("کاربر یافت نشد.");
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(UpdateUserDto dto)
        {
            var user = await repo.GetByIdAsync(dto.Id);
            if (user == null) throw new NotFoundException("کاربر یافت نشد.");
            mapper.Map(dto, user);
            await repo.UpdateEntity(user);
            await repo.SaveChangesAsync();
            return mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            logger.LogInformation("getting all users");
            var users = await repo.GetAllAsync();
            return mapper.Map<List<UserDto>>(users);
        }

        public async Task DeleteAsync(long id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null) throw new NotFoundException("کاربر یافت نشد.");
            user.IsDeleted = true;
            user.IsActive = false;
            await repo.UpdateEntity(user);
            await repo.SaveChangesAsync();
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await repo.GetSingleAsync(u => u.Email == email && !u.IsDeleted);
            if (user == null) return false;
            return VerifyPassword(password, user.PasswordHash);
        }

        public async Task ChangePasswordAsync(long userId, string newPassword)
        {
            var user = await repo.GetByIdAsync(userId);
            if (user == null) throw new NotFoundException("کاربر یافت نشد.");
            user.PasswordHash = HashPassword(newPassword);
            await repo.UpdateEntity(user);
            await repo.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public async Task<List<string>> GetRolesAsync(long userId)
        {
            var user = await repo.GetSingleAsync(u => u.Id == userId, includeString:"UserRoles");
            if (user == null) return new List<string>();
            return user.UserRoles?.Select(ur => ur.Role?.Name).Where(r => r != null).ToList() ?? new List<string>();
        }
        
        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}