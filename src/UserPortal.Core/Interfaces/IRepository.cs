using System;
using System.Linq.Expressions;
using UserPortal.Core.Results;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Core.Interfaces;


/// <summary>
/// Interface representing generic repository type.
/// </summary>
/// <typeparam name="TEntity">Type of entity which must implement IEntity and be a concrete class.</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Finds the first entity that matches the specified predicate or returns null if none found.
    /// </summary>
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets paginated records from the database asynchronously.
    /// </summary>
    /// <param name="pageSize">Number of records to return per page.</param>
    /// <param name="page">Page number (1-based).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns paginated records.</returns>
    Task<PaginatedResult<TEntity>> GetAllAsync(int pageSize, int page, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets paginated records that match the specified predicate.
    /// </summary>
    /// <param name="predicate">Predicate to filter records.</param>
    /// <param name="pageSize">Number of records to return per page.</param>
    /// <param name="page">Page number (1-based).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns paginated records that match the criteria.</returns>
    Task<PaginatedResult<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate, int pageSize, int page, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets entity with specified Id or returns null.
    /// </summary>
    /// <param name="id">Unique identifier of the entity to find.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns entity with specified id or null if not found.</returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns the added entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown when entity is null.</exception>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get total number of entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns total number of the entities, returns zero if none found.</returns>
    Task<int> GetCountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <param name="entity">Entity to be deleted.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if entity deleted successfully, false if entity not found.</returns>
    void Delete(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks whether an entity exists.
    /// </summary>
    /// <param name="id">Unique identifier of the entity to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if entity exists, false otherwise.</returns>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates entity with new values asynchronously.
    /// </summary>
    /// <param name="entity">Entity to be updated with new values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <remarks>It searches using the entity's ID which is required and cannot be updated.</remarks>
    /// <returns>Returns true if entity found and updated, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when entity is null.</exception>
    void Update(TEntity entity, CancellationToken cancellationToken = default);
}
