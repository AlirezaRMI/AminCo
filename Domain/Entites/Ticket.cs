using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;
using Domain.Enums;

namespace Domain.Entites
{
    [Table("Tickets", Schema = "Support")]
    public class Ticket : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;
        public long CreatedByUserId { get; set; }      
        public long? AssignedToUserId { get; set; }     
        public DateTime? AssignedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public virtual User CreatedByUser { get; set; } = null!;
        public virtual User? AssignedToUser { get; set; }
        public virtual ICollection<TicketReply> Replies { get; set; } = new List<TicketReply>();
        public virtual ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();
    }
    
}