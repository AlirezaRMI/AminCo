using Domain.Enums;

namespace Application.DTOs.Ticket
{
    public class TicketCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    }
    public class TicketReplyCreateDto
    {
        public long TicketId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsInternalNote { get; set; } = false;
    }
    public class TicketFilterDto
    {
        public TicketStatus? Status { get; set; }
        public TicketPriority? Priority { get; set; }
        public bool? AssignedToMe { get; set; }  
        public string? TitleContains { get; set; }
    }
    public class AttachmentFileDto
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
    }
}