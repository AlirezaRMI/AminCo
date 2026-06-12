using Application.DTOs.Common;

namespace Application.DTOs.Users
{
    public class UserDto : BaseDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }
    }

    public class RegisterUserDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public string Email { get; set; }
    }
}