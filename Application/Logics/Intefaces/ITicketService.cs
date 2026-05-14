using Application.DTOs.Ticket;
using Domain.Enums;

namespace Application.Logics.Intefaces
{
    public interface ITicketService
    {
        Task<TicketResponseDto> CreateTicketAsync(TicketCreateDto dto, List<AttachmentFileDto>? attachments = null);
        Task<TicketResponseDto> GetTicketByIdAsync(long ticketId);
        Task<TicketResponseDto> AddReplyAsync(TicketReplyCreateDto replyDto, List<AttachmentFileDto>? attachments = null);
        Task ChangeTicketStatusAsync(long ticketId, TicketStatus newStatus);
        Task TakeTicketAsync(long ticketId);
        Task<IReadOnlyList<TicketSummaryDto>> GetMyTicketsAsync(TicketFilterDto? filter = null);
        Task<IReadOnlyList<TicketSummaryDto>> GetAllTicketsAsync(TicketFilterDto? filter = null);
    }
}