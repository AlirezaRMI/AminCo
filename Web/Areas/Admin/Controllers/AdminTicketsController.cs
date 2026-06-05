using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Logics.Intefaces;
using Application.DTOs.Ticket;
using Domain.Enums;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminTicketsController(ITicketService ticketService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var tickets = await ticketService.GetAllTicketsAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Details(long id)
        {
            var ticket = await ticketService.GetTicketByIdAsync(id);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReply(long ticketId, string message, bool isInternalNote = false)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                TempData["Error"] = "متن پاسخ نمی‌تواند خالی باشد.";
                return RedirectToAction(nameof(Details), new { id = ticketId });
            }

            var replyDto = new TicketReplyCreateDto
            {
                TicketId = ticketId,
                Message = message,
                IsInternalNote = isInternalNote
            };
            await ticketService.AddReplyAsync(replyDto, null);
            TempData["Success"] = "پاسخ با موفقیت ثبت شد.";
            return RedirectToAction(nameof(Details), new { id = ticketId });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(long id, TicketStatus status)
        {
            await ticketService.ChangeTicketStatusAsync(id, status);
            TempData["Success"] = $"وضعیت تیکت به {status} تغییر یافت.";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> TakeTicket(long id)
        {
            await ticketService.TakeTicketAsync(id);
            TempData["Success"] = "تیکت به شما اختصاص یافت.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}