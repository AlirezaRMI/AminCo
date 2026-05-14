using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("TicketReplies", Schema = "Support")]
    public class TicketReply : BaseEntity
    {
        public long TicketId { get; set; }
        public long UserId { get; set; }          
        public string Message { get; set; } = string.Empty;
        public bool IsInternalNote { get; set; } 

        public virtual Ticket Ticket { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();
    }
}