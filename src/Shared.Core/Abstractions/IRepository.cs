namespace Shared.Core.Abstractions;

public interface IRepository<TEntity, in TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    Task<TEntity> SaveAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}

public interface IReadOnlyRepository<TEntity, in TKey>
{
    Task<TEntity> GetById(TKey id);
    Task<TEntity?> FindById(TKey id);
    IQueryable<TEntity> GetQueryable();
}