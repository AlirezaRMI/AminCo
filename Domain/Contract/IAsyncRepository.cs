using System.Linq.Expressions;
using Domain.Common;
using Domain.Entites.Base;


namespace Domain.Contract;

/// <summary>
/// Defines a generic asynchronous repository interface for entities with primary keys of type <typeparamref name="TKey"/>.
/// Supports advanced querying, soft-delete, transactions, and change tracking control.
/// </summary>
/// <typeparam name="TEntity">The entity type that inherits from <see cref="BaseEntity"/> and implements <see cref="IEntity{TKey}"/>.</typeparam>
/// <typeparam name="TKey">The type of the entity's primary key (typically Guid).</typeparam>
public interface IAsyncRepository<TEntity, TKey> : IAsyncDisposable
    where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <returns>A transaction object that can be committed or rolled back.</returns>
    Task<IDatabaseTransaction> BeginTransactionAsync();

    /// <summary>
    /// Returns the base IQueryable for the entity set (without any filters applied yet).
    /// Useful for building complex queries manually.
    /// </summary>
    IQueryable<TEntity> GetQuery();
    
    IQueryable<TEntity> GetQuery(bool ignoreQueryFilters = false);


    /// <summary>
    /// Retrieves all entities as a read-only list (no tracking by default).
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves all entities that match the given predicate.
    /// </summary>
    /// <param name="predicate">Filter expression.</param>
    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves entities with optional filtering, ordering, include paths, and tracking control.
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeString = null,
        bool disableTracking = true);

    /// <summary>
    /// Retrieves entities with optional filtering, ordering, multiple include expressions, and tracking control.
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        List<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = true);

    /// <summary>
    /// Returns the total number of entities in the set.
    /// </summary>
    Task<long> CountAsync();

    /// <summary>
    /// Returns the number of entities that match the predicate.
    /// </summary>
    Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Adds a new entity to the context (does NOT call SaveChanges).
    /// </summary>
    Task<TEntity> AddEntity(TEntity entity);

    /// <summary>
    /// Adds multiple entities to the context (does NOT call SaveChanges).
    /// </summary>
    Task<IEnumerable<TEntity>> AddEntities(IEnumerable<TEntity> entities);

    /// <summary>
    /// Retrieves a single entity matching the predicate (returns null if not found).
    /// </summary>
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Retrieves a single entity with optional string-based include path.
    /// </summary>
    Task<TEntity?> GetSingleAsync(
        Expression<Func<TEntity, bool>> predicate,
        string? includeString = null,
        bool disableTracking = true);

    /// <summary>
    /// Retrieves a single entity with optional lambda-based includes.
    /// </summary>
    Task<TEntity?> GetSingleAsync(
        Expression<Func<TEntity, bool>> predicate,
        List<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = true);

    /// <summary>
    /// Retrieves an entity by its primary key.
    /// </summary>
    Task<TEntity?> GetByIdAsync(TKey id);

    /// <summary>
    /// Retrieves an entity by ID with string-based include path.
    /// </summary>
    Task<TEntity?> GetByIdAsync(
        TKey id,
        string? includeString = null,
        bool disableTracking = true);

    /// <summary>
    /// Retrieves an entity by ID with lambda-based includes.
    /// </summary>
    Task<TEntity?> GetByIdAsync(
        TKey id,
        List<Expression<Func<TEntity, object>>>? includes = null,
        bool disableTracking = true);

    /// <summary>
    /// Marks an entity as modified (does NOT call SaveChanges).
    /// </summary>
    Task UpdateEntity(TEntity entity);

    /// <summary>
    /// Marks multiple entities as modified (does NOT call SaveChanges).
    /// </summary>
    Task UpdateEntities(IEnumerable<TEntity> entities);

    /// <summary>
    /// Performs a soft-delete on the entity (marks as deleted, does NOT call SaveChanges).
    /// </summary>
    void DeleteEntity(TEntity entity);

    /// <summary>
    /// Performs a soft-delete by primary key (fetches entity first).
    /// </summary>
    Task DeleteEntity(TKey entityId);

    /// <summary>
    /// Performs a permanent (hard) delete on the entity.
    /// </summary>
    void DeletePermanent(TEntity entity);

    /// <summary>
    /// Performs a permanent delete by primary key.
    /// </summary>
    Task DeletePermanent(TKey entityId);

    /// <summary>
    /// Soft-deletes multiple entities.
    /// </summary>
    Task Deletes(List<TEntity> entities);

    /// <summary>
    /// Commits all pending changes to the database.
    /// Usually called via UnitOfWork, not directly from services.
    /// </summary>
    Task SaveChangesAsync();

    /// <summary>
    /// Checks if any entity matches the given condition.
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Checks existence with string-based include.
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, string includeString);

    /// <summary>
    /// Checks existence with lambda-based includes.
    /// </summary>
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        List<Expression<Func<TEntity, object>>>? includes);


    IQueryable<TEntity> QueryWithIncludes(
        bool ignoreFilters = false,
        bool asNoTracking = true,
        params Expression<Func<TEntity, object>>[] includes);

}