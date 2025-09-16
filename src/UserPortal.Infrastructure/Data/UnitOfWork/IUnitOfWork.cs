using System;
using UserPortal.Core.Entities;
using UserPortal.Core.Interfaces;

namespace UserPortal.Infrastructure.Data.UnitOfWork;


/// <summary>
/// Interface representing the Unit of Work pattern for managing database transactions.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Gets the repository for managing user entities.
    /// </summary>
    IRepository<User> Users { get; }

    /// <summary>
    /// Commits all pending changes to the database within a transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>True if the commit was successful, false otherwise.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the database context is not available.</exception>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back all pending changes, reverting the current transaction.
    /// </summary>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new transaction for the current unit of work.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>True if the transaction was successfully started, false otherwise.</returns>
    Task<bool> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
