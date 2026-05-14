using Application.DTOs.Notification;
using Application.Logics.Intefaces;
using Domain.Common;
using Domain.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController(INotificationService notifService, IUserContextService userContext)
        : ControllerBase
    {
        private long CurrentUserId => userContext.UserId;

        [HttpGet]
        public async Task<ApiResult<IReadOnlyList<NotificationDto>>> GetMyNotifications([FromQuery] bool onlyUnread = false)
        {
            var notifs = await notifService.GetUserNotificationsAsync(CurrentUserId, onlyUnread);
            return new ApiResult<IReadOnlyList<NotificationDto>>(true, ApiResultStatusCode.Success, notifs);
        }

        [HttpGet("unread-count")]
        public async Task<int> GetUnreadCount()
        {
            var count = await notifService.GetUnreadCountAsync(CurrentUserId);
            return count;
        }

        [HttpPost("{id}/read")]
        public async Task<ApiResult> MarkAsRead(long id)
        {
            await notifService.MarkAsReadAsync(id);
            return new OkResult();
        }

        [HttpPost("read-all")]
        public async Task<ApiResult> MarkAllAsRead()
        {
            await notifService.MarkAllAsReadAsync(CurrentUserId);
            return new OkResult();
        }
    }
}