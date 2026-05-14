using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites;

[Table("UserRoles", Schema = "Users")]
public class UserRole : BaseEntity
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}