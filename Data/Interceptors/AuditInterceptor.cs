using Microsoft.EntityFrameworkCore.Diagnostics;
using Domain.Contract;
using Domain.Entites.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Data.Interceptors;
/// <summary>
/// EF Core SaveChanges interceptor that automatically stamps audit fields
/// (<see cref="BaseEntity.UpdatedAt"/> and <see cref="BaseEntity.UpdatedBy"/>)
/// on every entity that is being added or modified before persisting to the database.
/// </summary>
/// <remarks>
/// <list type="bullet">
///   <item>Hooks into the EF Core pipeline via <see cref="SaveChangesInterceptor"/>.</item>
///   <item>Iterates over all tracked entities with state <b>Added</b> or <b>Modified</b>.</item>
///   <item>Only affects entities inheriting from <see cref="BaseEntity"/>.</item>
///   <item>Sets <c>UpdatedAt</c> to <see cref="DateTime.UtcNow"/> and <c>UpdatedBy</c> to the current user's ID.</item>
///   <item>Runs automatically on every <c>SaveChangesAsync</c> call — no manual intervention needed.</item>
/// </list>
/// </remarks>
public class AuditInterceptor(
    IUserContextService userContext,
    ILogger<AuditInterceptor> logger)
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result, CancellationToken ct)
    {
        var entries = eventData.Context!.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
            .ToList();
        var auditedCount = 0;
        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity entity)
            {
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = userContext.UserId;
                auditedCount++;
            }
        }
        if (auditedCount > 0)
        {
            logger.LogDebug(
                "Audit fields stamped | AuditedEntities: {AuditedCount} | TotalTracked: {TotalCount} | UserId: {UserId}",
                auditedCount, entries.Count, userContext.UserId);
        }
        return base.SavingChangesAsync(eventData, result, ct);
    }
}