using System;
using Microsoft.EntityFrameworkCore;
using UserPortal.Core.Entities;
using UserPortal.Core.Interfaces;
using UserPortal.Infrastructure.Data.Repositories;

namespace UserPortal.Infrastructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    public IRepository<User> Users => new Repository<User>(_context);

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<bool> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CommitChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            _context?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            await DisposeAsyncCore().ConfigureAwait(false);
            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }

    protected virtual async Task DisposeAsyncCore()
    {
        if (_context != null)
            await _context.DisposeAsync().ConfigureAwait(false);
    }

    public Task RollbackAsync()
    {
        throw new NotImplementedException();
    }
}
