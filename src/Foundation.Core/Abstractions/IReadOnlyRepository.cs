namespace Foundation.Core.Abstractions;

public interface IReadOnlyRepository<TEntity, in TKey>
{
    Task<TEntity> GetById(TKey id, CancellationToken cancellationToken);
    Task<TEntity?> FindById(TKey id, CancellationToken cancellationToken);
    IQueryable<TEntity> GetQueryable();
    IQueryable<TEntity> GetQueryableIncludingDeleted();
}