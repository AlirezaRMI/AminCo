using Domain.Enums;

namespace Application.DTOs.Ticket
{
    public class TicketResponseDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public long CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } = string.Empty;
        public long? AssignedToUserId { get; set; }
        public string? AssignedToUserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public List<TicketReplyDto> Replies { get; set; } = new();
        public List<TicketAttachmentDto> Attachments { get; set; } = new();
    }
}