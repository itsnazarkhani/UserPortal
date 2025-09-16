using System;
using Microsoft.EntityFrameworkCore;
using UserPortal.Core.Entities;
using UserPortal.Core.Interfaces;
using UserPortal.Infrastructure.Data.Repositories;

namespace UserPortal.Infrastructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public IRepository<User> Users { get; init; }

    public UnitOfWork(ApplicationDbContext context)
    {
        Users = new Repository<User>(context);
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
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public Task RollbackAsync()
    {
        throw new NotImplementedException();
    }
}
