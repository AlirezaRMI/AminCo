using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("ContactInfo", Schema = "Content")] 
    public class ContactInfo : BaseEntity
    {
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? WorkingHours { get; set; }
        public string? GoogleMapUrl { get; set; }
    }
}