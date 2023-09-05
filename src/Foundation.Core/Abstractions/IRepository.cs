namespace Foundation.Core.Abstractions;

public interface IRepository<TEntity, in TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}

public interface IReadOnlyRepository<TEntity, in TKey>
{
    Task<TEntity> GetById(TKey id, CancellationToken cancellationToken);
    Task<TEntity?> FindById(TKey id, CancellationToken cancellationToken);
    IQueryable<TEntity> GetQueryable();
    IQueryable<TEntity> GetQueryableIncludingDeleted();
}