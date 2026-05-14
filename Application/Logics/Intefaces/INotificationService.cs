using Application.DTOs.Notification;

namespace Application.Logics.Intefaces
{
    public interface INotificationService
    {
        Task SendAsync(long userId, string title, string message, string? link = null, string? entityType = null, long? entityId = null);
        Task SendToAdminsAsync(string title, string message, string? link = null, string? entityType = null, long? entityId = null);
        Task<IReadOnlyList<NotificationDto>> GetUserNotificationsAsync(long userId, bool onlyUnread = false);
        Task MarkAsReadAsync(long notificationId);
        Task MarkAllAsReadAsync(long userId);
        Task<int> GetUnreadCountAsync(long userId);
    }
}