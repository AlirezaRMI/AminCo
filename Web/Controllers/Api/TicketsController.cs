using Application.DTOs.Ticket;
using Application.Logics.Intefaces;
using Domain.Common;
using Domain.Contract;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController(
        ITicketService ticketService,
        IFileUploadService fileUploadService,
        IUserContextService userContext)
        : ControllerBase
    {
        private long CurrentUserId => userContext.UserId;
        private bool IsAdmin => userContext.Roles.Contains("Admin");

        [HttpGet]
        public async Task<ApiResult<IReadOnlyList<TicketSummaryDto>>> GetMyTickets([FromQuery] TicketFilterDto? filter)
        {
            var tickets = await ticketService.GetMyTicketsAsync(filter);
            return new ApiResult<IReadOnlyList<TicketSummaryDto>>(true, ApiResultStatusCode.Success, tickets);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<TicketResponseDto>> GetById(long id)
        {
            var ticket = await ticketService.GetTicketByIdAsync(id);
            return ticket;
        }

        [HttpPost]
        public async Task<ApiResult<TicketResponseDto>> Create([FromForm] TicketCreateDto dto, [FromForm] List<IFormFile>? attachments)
        {
            var attachmentDtos = new List<AttachmentFileDto>();
            if (attachments != null)
            {
                foreach (var file in attachments)
                {
                    var path = await fileUploadService.UploadFileAsync(file, $"tickets/{CurrentUserId}");
                    attachmentDtos.Add(new AttachmentFileDto
                    {
                        FileName = file.FileName,
                        FilePath = path,
                        FileSize = file.Length,
                        ContentType = file.ContentType
                    });
                }
            }
            var result = await ticketService.CreateTicketAsync(dto, attachmentDtos);
            return result;
        }

        [HttpPost("{id}/reply")]
        public async Task<ApiResult<TicketResponseDto>> AddReply(long id, [FromForm] string message, [FromForm] bool isInternalNote, [FromForm] List<IFormFile>? attachments)
        {
            var replyDto = new TicketReplyCreateDto
            {
                TicketId = id,
                Message = message,
                IsInternalNote = isInternalNote
            };
            var attachmentDtos = new List<AttachmentFileDto>();
            if (attachments != null)
            {
                foreach (var file in attachments)
                {
                    var path = await fileUploadService.UploadFileAsync(file, $"tickets/{id}/replies");
                    attachmentDtos.Add(new AttachmentFileDto
                    {
                        FileName = file.FileName,
                        FilePath = path,
                        FileSize = file.Length,
                        ContentType = file.ContentType
                    });
                }
            }
            var result = await ticketService.AddReplyAsync(replyDto, attachmentDtos);
            return result;
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> ChangeStatus(long id, [FromBody] TicketStatus status)
        {
            await ticketService.ChangeTicketStatusAsync(id, status);
            return new OkResult();
        }

        [HttpPost("{id}/take")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> TakeTicket(long id)
        {
            await ticketService.TakeTicketAsync(id);
            return new OkResult();
        }

        [HttpGet("admin/all")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<IReadOnlyList<TicketSummaryDto>>> GetAllTickets([FromQuery] TicketFilterDto? filter)
        {
            var tickets = await ticketService.GetAllTicketsAsync(filter);
            return new ApiResult<IReadOnlyList<TicketSummaryDto>>(true, ApiResultStatusCode.Success, tickets);
        }
    }
}