using Domain.Contract;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Ripositores
{
    public class EfCoreTransaction(IDbContextTransaction transaction) : IDatabaseTransaction
    {
        private readonly IDbContextTransaction _transaction = transaction;

        public async Task CommitAsync(CancellationToken cancellationToken = default)
            => await _transaction.CommitAsync(cancellationToken);

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
            => await _transaction.RollbackAsync(cancellationToken);

        public async ValueTask DisposeAsync()
            => await _transaction.DisposeAsync();
    }
}