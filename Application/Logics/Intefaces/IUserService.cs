using Application.DTOs.Users;

namespace Application.Logics.Intefaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterUserDto dto);
        Task<UserDto> GetByIdAsync(long id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<UserDto> UpdateAsync(UpdateUserDto dto);
        
        Task<List<UserDto>> GetAllAsync();
        Task DeleteAsync(long id);
        Task<bool> ValidateCredentialsAsync(string email, string password);
        Task ChangePasswordAsync(long userId, string newPassword);
    }
}