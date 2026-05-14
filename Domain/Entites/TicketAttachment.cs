using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entites.Base;

namespace Domain.Entites
{
    [Table("TicketAttachments", Schema = "Support")]
    public class TicketAttachment : BaseEntity
    {
        public long TicketId { get; set; }
        public long? TicketReplyId { get; set; }  
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;  
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;

        public virtual Ticket Ticket { get; set; } = null!;
        public virtual TicketReply? TicketReply { get; set; }
    }
}