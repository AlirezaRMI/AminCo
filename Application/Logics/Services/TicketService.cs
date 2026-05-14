using Application.DTOs.Ticket;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class TicketService(
        IAsyncRepository<Ticket, long> ticketRepo,
        IAsyncRepository<TicketReply, long> replyRepo,
        IAsyncRepository<TicketAttachment, long> attachmentRepo,
        IUserContextService userContext,
        INotificationService notificationService,
        IMapper mapper,
        ILogger<TicketService> logger)
        : ITicketService
    {

        private long CurrentUserId => userContext.UserId;
        private bool IsAdmin => userContext.Roles.Contains("Admin");

        public async Task<TicketResponseDto> CreateTicketAsync(TicketCreateDto dto, List<AttachmentFileDto>? attachments = null)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = TicketStatus.Open,
                CreatedByUserId = CurrentUserId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            await ticketRepo.AddEntity(ticket);
            await ticketRepo.SaveChangesAsync();

            // ذخیره پیوست‌های سطح تیکت (بدون reply)
            if (attachments != null)
            {
                foreach (var att in attachments)
                {
                    var attachment = new TicketAttachment
                    {
                        TicketId = ticket.Id,
                        FileName = att.FileName,
                        FilePath = att.FilePath,
                        FileSize = att.FileSize,
                        ContentType = att.ContentType
                    };
                    await attachmentRepo.AddEntity(attachment);
                }
                await attachmentRepo.SaveChangesAsync();
            }

            // اعلان به تمام ادمین‌ها (بعداً پیاده‌سازی می‌شود)
            await notificationService.SendToAdminsAsync("تیکت جدید", $"تیکت '{ticket.Title}' ایجاد شد.", $"/Admin/Tickets/Details/{ticket.Id}", "Ticket", ticket.Id);

            return await GetTicketByIdAsync(ticket.Id);
        }

        public async Task<TicketResponseDto> GetTicketByIdAsync(long ticketId)
        {
            var ticket = await ticketRepo.GetSingleAsync(
                t => t.Id == ticketId,
                includes: new() { x => x.CreatedByUser, x => x.AssignedToUser, x => x.Replies, x => x.Attachments });
            if (ticket == null) throw new NotFoundException("تیکت یافت نشد.");

            if (!IsAdmin && ticket.CreatedByUserId != CurrentUserId)
                throw new UnauthorizedAccessException("شما اجازه مشاهده این تیکت را ندارید.");

            var dto = mapper.Map<TicketResponseDto>(ticket);
            // بارگذاری پاسخ‌ها و پیوست‌ها (AutoMapper باید این کار را انجام دهد)
            return dto;
        }

        public async Task<TicketResponseDto> AddReplyAsync(TicketReplyCreateDto replyDto, List<AttachmentFileDto>? attachments = null)
        {
            var ticket = await ticketRepo.GetByIdAsync(replyDto.TicketId);
            if (ticket == null) throw new NotFoundException("تیکت یافت نشد.");

            if (!IsAdmin && ticket.CreatedByUserId != CurrentUserId)
                throw new UnauthorizedAccessException("شما نمی‌توانید به این تیکت پاسخ دهید.");

            // اگر پیوست سطح reply وجود داشت، ذخیره کن
            var reply = new TicketReply
            {
                TicketId = replyDto.TicketId,
                UserId = CurrentUserId,
                Message = replyDto.Message,
                IsInternalNote = replyDto.IsInternalNote && IsAdmin, // فقط ادمین می‌تواند یادداشت داخلی بگذارد
                CreatedAt = DateTime.UtcNow
            };
            await replyRepo.AddEntity(reply);
            await replyRepo.SaveChangesAsync();

            if (attachments != null)
            {
                foreach (var att in attachments)
                {
                    var attachment = new TicketAttachment
                    {
                        TicketId = replyDto.TicketId,
                        TicketReplyId = reply.Id,
                        FileName = att.FileName,
                        FilePath = att.FilePath,
                        FileSize = att.FileSize,
                        ContentType = att.ContentType
                    };
                    await attachmentRepo.AddEntity(attachment);
                }
                await attachmentRepo.SaveChangesAsync();
            }

            // بروزرسانی وضعیت تیکت در صورت نیاز
            if (IsAdmin && ticket.AssignedToUserId == null)
            {
                ticket.AssignedToUserId = CurrentUserId;
                ticket.AssignedAt = DateTime.UtcNow;
                ticket.Status = TicketStatus.InProgress;
                await ticketRepo.UpdateEntity(ticket);
                await ticketRepo.SaveChangesAsync();
            }

            // ارسال اعلان به گیرنده مناسب
            if (IsAdmin)
            {
                // اعلان به کاربر ایجادکننده تیکت
                await notificationService.SendAsync(ticket.CreatedByUserId, "پاسخ جدید به تیکت شما",
                    $"به تیکت '{ticket.Title}' پاسخی داده شد.", $"/Tickets/Details/{ticket.Id}", "Ticket", ticket.Id);
            }
            else
            {
                if (ticket.AssignedToUserId.HasValue)
                {
                    await notificationService.SendAsync(ticket.AssignedToUserId.Value, "پاسخ کاربر به تیکت",
                        $"کاربر به تیکت '{ticket.Title}' پاسخ داد.", $"/Admin/Tickets/Details/{ticket.Id}", "Ticket", ticket.Id);
                }
                else
                {
                    await notificationService.SendToAdminsAsync("پاسخ کاربر به تیکت",
                        $"کاربر به تیکت '{ticket.Title}' پاسخ داد.", $"/Admin/Tickets/Details/{ticket.Id}", "Ticket", ticket.Id);
                }
            }

            return await GetTicketByIdAsync(ticket.Id);
        }

        public async Task ChangeTicketStatusAsync(long ticketId, TicketStatus newStatus)
        {
            if (!IsAdmin) throw new UnauthorizedAccessException("فقط ادمین می‌تواند وضعیت تیکت را تغییر دهد.");
            var ticket = await ticketRepo.GetByIdAsync(ticketId);
            if (ticket == null) throw new NotFoundException("تیکت یافت نشد.");

            ticket.Status = newStatus;
            if (newStatus == TicketStatus.Resolved) ticket.ResolvedAt = DateTime.UtcNow;
            if (newStatus == TicketStatus.Closed) ticket.ClosedAt = DateTime.UtcNow;

            await ticketRepo.UpdateEntity(ticket);
            await ticketRepo.SaveChangesAsync();

            await notificationService.SendAsync(ticket.CreatedByUserId, "وضعیت تیکت تغییر کرد",
                $"وضعیت تیکت '{ticket.Title}' به {newStatus} تغییر یافت.", $"/Tickets/Details/{ticket.Id}", "Ticket", ticket.Id);
        }

        public async Task TakeTicketAsync(long ticketId)
        {
            if (!IsAdmin) throw new UnauthorizedAccessException("فقط ادمین می‌تواند تیکت را به خود اختصاص دهد.");
            var ticket = await ticketRepo.GetByIdAsync(ticketId);
            if (ticket == null) throw new NotFoundException("تیکت یافت نشد.");
            if (ticket.AssignedToUserId != null) throw new BadRequestException("این تیکت قبلاً به ادمین دیگری اختصاص یافته است.");

            ticket.AssignedToUserId = CurrentUserId;
            ticket.AssignedAt = DateTime.UtcNow;
            ticket.Status = TicketStatus.InProgress;
            await ticketRepo.UpdateEntity(ticket);
            await ticketRepo.SaveChangesAsync();

            await notificationService.SendAsync(ticket.CreatedByUserId, "تیکت توسط ادمین گرفته شد",
                $"تیکت '{ticket.Title}' توسط ادمین بررسی می‌شود.", $"/Tickets/Details/{ticket.Id}", "Ticket", ticket.Id);
        }

        public async Task<IReadOnlyList<TicketSummaryDto>> GetMyTicketsAsync(TicketFilterDto? filter = null)
        {
            var query = ticketRepo.GetQuery().Where(t => t.CreatedByUserId == CurrentUserId);
            if (filter != null)
            {
                if (filter.Status.HasValue) query = query.Where(t => t.Status == filter.Status.Value);
                if (filter.Priority.HasValue) query = query.Where(t => t.Priority == filter.Priority.Value);
                if (!string.IsNullOrEmpty(filter.TitleContains)) query = query.Where(t => t.Title.Contains(filter.TitleContains));
            }
            var tickets = query.OrderByDescending(t => t.CreatedAt).ToList();
            return mapper.Map<IReadOnlyList<TicketSummaryDto>>(tickets);
        }

        public async Task<IReadOnlyList<TicketSummaryDto>> GetAllTicketsAsync(TicketFilterDto? filter = null)
        {
            if (!IsAdmin) throw new UnauthorizedAccessException("فقط ادمین می‌تواند همه تیکت‌ها را ببیند.");
            var query = ticketRepo.GetQuery();
            if (filter != null)
            {
                if (filter.Status.HasValue) query = query.Where(t => t.Status == filter.Status.Value);
                if (filter.Priority.HasValue) query = query.Where(t => t.Priority == filter.Priority.Value);
                if (filter.AssignedToMe == true) query = query.Where(t => t.AssignedToUserId == CurrentUserId);
                if (!string.IsNullOrEmpty(filter.TitleContains)) query = query.Where(t => t.Title.Contains(filter.TitleContains));
            }
            var tickets =  query.OrderByDescending(t => t.CreatedAt).ToList();
            return mapper.Map<IReadOnlyList<TicketSummaryDto>>(tickets);
        }
    }
}