namespace Application.DTOs.Ticket
{
    public class TicketReplyDto
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsInternalNote { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TicketAttachmentDto> Attachments { get; set; } = new();
    }
}