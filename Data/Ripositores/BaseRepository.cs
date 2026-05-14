using System.Linq.Expressions;
using Data.Context;
using Domain.Common;
using Domain.Contract;
using Microsoft.EntityFrameworkCore;

namespace Data.Ripositores
{
    /// <summary>
    /// Generic repository implementation with two generic parameters (TEntity, TKey).
    /// TKey is typically 'long' for Aminco project.
    /// Supports transactions, soft-delete (if needed), and permanent delete.
    /// </summary>
    public class BaseRepository<TEntity, TKey>(AmincoDbContext context) : IAsyncRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly AmincoDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        // ──────────────────────────── Transaction ────────────────────────────
        public async Task<IDatabaseTransaction> BeginTransactionAsync()
        {
            var efTransaction = await _context.Database.BeginTransactionAsync();
            return new EfCoreTransaction(efTransaction);
        }

        // ──────────────────────────── Query Helpers ────────────────────────────
        public IQueryable<TEntity> GetQuery()
        {
            return _context.Set<TEntity>().AsQueryable().AsNoTracking();
        }

        public IQueryable<TEntity> GetQuery(bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            return query;
        }

        public IQueryable<TEntity> QueryWithIncludes(
            bool ignoreFilters = false,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (ignoreFilters)
                query = query.IgnoreQueryFilters();
            if (asNoTracking)
                query = query.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query;
        }

        // ──────────────────────────── Get / Fetch ────────────────────────────
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true)
        {
            var query = GetQuery();
            if (disableTracking)
                query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<Expression<Func<TEntity, object>>>? includes = null,
            bool disableTracking = true)
        {
            var query = GetQuery();
            if (disableTracking)
                query = query.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetSingleAsync(predicate, includeString: null);
        }

        public async Task<TEntity?> GetSingleAsync(
            Expression<Func<TEntity, bool>> predicate,
            string? includeString = null,
            bool disableTracking = true)
        {
            var query = GetQuery();
            if (disableTracking)
                query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);
            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetSingleAsync(
            Expression<Func<TEntity, bool>> predicate,
            List<Expression<Func<TEntity, object>>>? includes = null,
            bool disableTracking = true)
        {
            var query = GetQuery();
            if (disableTracking)
                query = query.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetByIdAsync(
            TKey id,
            string? includeString = null,
            bool disableTracking = true)
        {
            var query = GetQuery();
            if (disableTracking)
                query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);
            return await query.SingleOrDefaultAsync(e => e.Id!.Equals(id));
        }

        public async Task<TEntity?> GetByIdAsync(
            TKey id,
            List<Expression<Func<TEntity, object>>>? includes = null,
            bool disableTracking = true)
        {
            var query = GetQuery();
            if (disableTracking)
                query = query.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(e => e.Id!.Equals(id));
        }

        // ──────────────────────────── Count / Any ────────────────────────────
        public async Task<long> CountAsync()
        {
            return await _context.Set<TEntity>().LongCountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().LongCountAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, string includeString)
        {
            var query = GetQuery();
            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);
            return await query.AnyAsync(predicate);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate,
            List<Expression<Func<TEntity, object>>>? includes)
        {
            var query = GetQuery();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.AnyAsync(predicate);
        }

        // ──────────────────────────── Add / Update / Delete ────────────────────────────
        public async Task<TEntity> AddEntity(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddEntities(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public async Task UpdateEntity(TEntity entity)
        {
            _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task UpdateEntities(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Hard delete (physical removal). For soft delete, override in derived repository.
        /// </summary>
        public void DeleteEntity(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteEntity(TKey entityId)
        {
            var entity = await GetByIdAsync(entityId);
            if (entity != null)
                DeleteEntity(entity);
        }

        public void DeletePermanent(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task DeletePermanent(TKey entityId)
        {
            var entity = await GetByIdAsync(entityId);
            if (entity != null)
                DeletePermanent(entity);
        }

        public async Task Deletes(List<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await Task.CompletedTask;
        }

        // ──────────────────────────── Save ────────────────────────────
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // ──────────────────────────── Dispose ────────────────────────────
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}