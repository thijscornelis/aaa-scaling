using Foundation.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Core.EntityFramework;

public class EntityFrameworkRepository<TEntity, TKey> : EntityFrameworkReadOnlyRepository<TEntity, TKey>,
    IRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    public EntityFrameworkRepository(DbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var existingEntity = await FindById(entity.Id, cancellationToken);
        if (existingEntity != null)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached) Set.Attach(entity);
            entry.State = EntityState.Modified;
        }
        else
        {
            await Set.AddAsync(entity, cancellationToken);
        }

        return entity;
    }

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Set.AddAsync(entity, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.Delete();
        return await SaveAsync(entity, cancellationToken);
    }
}