using System;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Application.Interfaces;

/// <summary>
/// Interface representing generic repository type.
/// </summary>
/// <typeparam name="TEntity">Type of entity which it should be of type IEntity and a concrete class.</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Gets all the records from the db asynchronously.
    /// </summary>
    /// <param name="take">Number of records to return.</param>
    /// <param name="page">Number or the page to take records from.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Retruns all the records.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(int take, int page, CancellationToken cancellationToken = default);
    /// <summary>
    /// Gets all the records that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">Predicate to search with.</param>
    /// <param name="take">Number of records to return.</param>
    /// <param name="page">Number or the page to take records from.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns all the records that match the ceriteria.</returns>
    Task<IEnumerable<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate, int take, int page, CancellationToken cancellationToken = default);
    /// <summary>
    /// Gets entitiy with specified Id or returns null.
    /// </summary>
    /// <param name="id">Unique identifier of the entity to find.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Returns entity with specified id or null if not found.</returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Get total number of entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Retuns total number of the entities, returns zero if found no one.</returns>
    int GetCount(CancellationToken cancellationToken = default);
    /// <summary>
    /// Deletes the specified entity.
    /// </summary>
    /// <param name="entity">Entity which to be deleted.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if entity deleted successfully, false if entity not-found.</returns>
    bool Delete(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Checks whether an entity exists or not.
    /// </summary>
    /// <param name="id">Unique identifier of the entity to check it's existence.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if entity exists, false otherwise.</returns>
    bool Exists(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Updates entity with new values provided asynchronously
    /// </summary>
    /// <param name="entity">Entity to be updated with new values.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <remarks>It searches with id of the entity therefore id is required and it does not gets updated.</remarks>
    /// <returns>Returns true if entity found and updated, false otherwise.</returns>
    Task<bool> UpdateAsync(TEntity entity);
}
