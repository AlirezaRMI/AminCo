using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("Users", Schema = "Users")]
    public class User : BaseEntity, IEntity<long>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}