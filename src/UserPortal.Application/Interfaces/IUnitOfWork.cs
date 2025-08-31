using System;
using UserPortal.Core.Entities;

namespace UserPortal.Application.Interfaces;

/// <summary>
/// Inteface representing unit of work pattern.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Repository of users
    /// </summary>
    IRepository<ApplicationUser> Users { get; }

    /// <summary>
    /// Commits changes which is made to db context to database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for cancel opration.</param>
    Task CommitChangesAsync(CancellationToken cancellationToken = default);
}
