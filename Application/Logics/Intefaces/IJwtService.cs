using Application.DTOs.Users;

namespace Application.Logics.Intefaces;

public interface IJwtService
{
    string GenerateToken(UserDto user, IReadOnlyList<string> roles);
}