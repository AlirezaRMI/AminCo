namespace Application.DTOs.Partner
{
    public class PartnerDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? Website { get; set; }
        public int DisplayOrder { get; set; }
    }
}