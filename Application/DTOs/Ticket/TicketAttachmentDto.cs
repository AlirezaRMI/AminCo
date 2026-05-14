namespace Application.DTOs.Ticket
{
    public class TicketAttachmentDto
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public long? TicketReplyId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}