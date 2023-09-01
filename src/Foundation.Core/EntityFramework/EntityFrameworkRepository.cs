using Foundation.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Core.EntityFramework;

public class EntityFrameworkRepository<TEntity, TKey> : EntityFrameworkReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    public EntityFrameworkRepository(DbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var existingEntity = await FindById(entity.Id);
        if (existingEntity != null)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached) Set.Attach(entity);
            entry.State = EntityState.Modified;
        }
        else
        {
            await Set.AddAsync(entity);
        }

        return entity;
    }

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Set.AddAsync(entity);
        return entity;
    }

    /// <inheritdoc />
    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.Delete();
        return await SaveAsync(entity);
    }
}