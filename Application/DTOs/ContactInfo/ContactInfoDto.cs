namespace Application.DTOs.ContactInfo
{
    public class ContactInfoDto
    {
        public long Id { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? WorkingHours { get; set; }
        public string? GoogleMapUrl { get; set; }
    }

    public class UpdateContactInfoDto
    {
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? WorkingHours { get; set; }
        public string? GoogleMapUrl { get; set; }
    }
}