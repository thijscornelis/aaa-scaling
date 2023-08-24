using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;

namespace Shared.Core.EntityFramework;

public abstract class EntityFrameworkRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _set;

    protected EntityFrameworkRepository(DbContext context)
    {
        _context = context;
        _set = _context.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<TEntity?> FindById(TKey id) => await _set.SingleOrDefaultAsync(x => x.Id.Equals(id));

    /// <inheritdoc />
    public async Task<TEntity> GetById(TKey id) => await _set.SingleAsync(x => x.Id.Equals(id));

    /// <inheritdoc />
    public IQueryable<TEntity> GetQueryable() => _set;

    /// <inheritdoc />
    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var existingEntity = await FindById(entity.Id);
        if (existingEntity != null)
        {
            existingEntity.SetModified();
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached) _set.Attach(entity);
            entry.State = EntityState.Modified;
        }
        else
        {
            await _set.AddAsync(entity);
        }

        return entity;
    }

    /// <inheritdoc />
    public Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.Delete();
        return SaveAsync(entity);
    }
}