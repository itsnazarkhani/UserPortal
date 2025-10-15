using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserPortal.Core.Interfaces;
using UserPortal.Core.Results;
using UserPortal.Infrastructure.Extensions;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Infrastructure.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public void Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);

    public async Task<PaginatedResult<TEntity>> GetAllAsync(int pageSize, int page, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
                           .PaginateAsync(pageSize, page, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
                           .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    public Task<PaginatedResult<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate, int pageSize, int page, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking().Where(predicate).AsQueryable();
        return query.PaginateAsync(pageSize, page, cancellationToken);
    }

    public void Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
    }
}
