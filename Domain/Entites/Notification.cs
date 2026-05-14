using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("Notifications", Schema = "Support")]
    public class Notification : BaseEntity
    {
        public long UserId { get; set; }     
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Link { get; set; }    
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public string? EntityType { get; set; } 
        public long? EntityId { get; set; }     

        public virtual User User { get; set; } = null!;
    }
}