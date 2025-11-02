using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.ComponentModel;
using UserPortal.Core.Interfaces;
using UserPortal.Infrastructure.Data.Repositories;
using UserPortal.Infrastructure.Identity;

namespace UserPortal.Infrastructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public IRepository<ApplicationUser> Users => new Repository<ApplicationUser>(_context);

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
            return false;

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return true;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) return;

        await _context.SaveChangesAsync(cancellationToken);
        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) return;

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    private bool _disposed = false;

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
}
