using Domain.Enums;

namespace Application.DTOs.Ticket
{
    public class TicketSummaryDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public string CreatedByUserName { get; set; } = string.Empty;
        public string? AssignedToUserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RepliesCount { get; set; }
    }
}