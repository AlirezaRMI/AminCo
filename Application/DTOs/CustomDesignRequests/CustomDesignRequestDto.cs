using Application.DTOs.Common;
using Domain.Enums;

namespace Application.DTOs.CustomDesignRequests
{
    public class CustomDesignRequestDto : BaseDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Attachments { get; set; }
        public RequestStatus Status { get; set; }
        public string? AdminResponse { get; set; }
    }

    public class CreateCustomDesignRequestDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Attachments { get; set; }
    }

    public class UpdateCustomDesignRequestDto
    {
        public long Id { get; set; }
        public RequestStatus Status { get; set; }
        public string? AdminResponse { get; set; }
    }
}