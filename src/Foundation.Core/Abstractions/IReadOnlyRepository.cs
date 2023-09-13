using System.Linq.Expressions;

namespace Foundation.Core.Abstractions;

public interface IReadOnlyRepository<TEntity, in TKey>
{
    Task<TEntity> GetById(TKey id, CancellationToken cancellationToken);
    Task<TEntity?> FindById(TKey id, CancellationToken cancellationToken);
    IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetQueryableIncludingDeleted(Expression<Func<TEntity, bool>> predicate);
}