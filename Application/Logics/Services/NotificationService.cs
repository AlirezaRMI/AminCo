using Application.DTOs.Notification;
using Application.Logics.Intefaces;
using AutoMapper;
using Domain.Contract;
using Domain.Entites;
using Microsoft.Extensions.Logging;

namespace Application.Logics.Services
{
    public class NotificationService(
        IAsyncRepository<Notification, long> notifRepo,
        IAsyncRepository<User, long> userRepo,
        IMapper mapper,
        ILogger<NotificationService> logger)
        : INotificationService
    {
        public async Task SendAsync(long userId, string title, string message, string? link = null, string? entityType = null, long? entityId = null)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Link = link,
                EntityType = entityType,
                EntityId = entityId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await notifRepo.AddEntity(notification);
            await notifRepo.SaveChangesAsync();
            logger.LogDebug("Notification sent to User {UserId}: {Title}", userId, title);
        }

        public async Task SendToAdminsAsync(string title, string message, string? link = null, string? entityType = null, long? entityId = null)
        {
            var admins = await userRepo.GetAsync(u => u.UserRoles.Any(ur => ur.Role.Name == "Admin") && !u.IsDeleted);
            foreach (var admin in admins)
            {
                await SendAsync(admin.Id, title, message, link, entityType, entityId);
            }
        }

        public async Task<IReadOnlyList<NotificationDto>> GetUserNotificationsAsync(long userId, bool onlyUnread = false)
        {
            var query = notifRepo.GetQuery().Where(n => n.UserId == userId);
            if (onlyUnread) query = query.Where(n => !n.IsRead);
            var notifications = query.OrderByDescending(n => n.CreatedAt).ToList();
            return mapper.Map<IReadOnlyList<NotificationDto>>(notifications);
        }

        public async Task MarkAsReadAsync(long notificationId)
        {
            var notif = await notifRepo.GetByIdAsync(notificationId);
            if (notif != null && !notif.IsRead)
            {
                notif.IsRead = true;
                notif.ReadAt = DateTime.UtcNow;
                await notifRepo.UpdateEntity(notif);
                await notifRepo.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsReadAsync(long userId)
        {
            var notifications = await notifRepo.GetAsync(n => n.UserId == userId && !n.IsRead);
            foreach (var n in notifications)
            {
                n.IsRead = true;
                n.ReadAt = DateTime.UtcNow;
                await notifRepo.UpdateEntity(n);
            }
            await notifRepo.SaveChangesAsync();
        }

        public async Task<int> GetUnreadCountAsync(long userId)
        {
            return (int) await notifRepo.CountAsync(n => n.UserId == userId && !n.IsRead);
        }
    }
}