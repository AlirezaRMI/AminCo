namespace Domain.Contract;


/// <summary>
/// Represents a database transaction that is independent of any specific data access technology.
/// </summary>
public interface IDatabaseTransaction : IAsyncDisposable
{
    /// <summary>
    /// Commits all changes made to the database in the current transaction.
    /// </summary>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Discards all changes made to the database in the current transaction.
    /// </summary>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}