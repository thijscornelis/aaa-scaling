using Foundation.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Foundation.Core.EntityFramework;

public class EntityFrameworkReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> Set;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityFrameworkReadOnlyRepository{TEntity,TKey}" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public EntityFrameworkReadOnlyRepository(DbContext context)
    {
        Context = context;
        Set = Context.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<TEntity?> FindById(TKey id, CancellationToken cancellationToken)
    {
        var entity = Set.Local.Where(x => !x.IsDeleted)
            .SingleOrDefault(x => x.Id.Equals(id));

        return entity ?? await Set.Where(x => !x.IsDeleted)
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> GetById(TKey id, CancellationToken cancellationToken)
    {
        var entity = Set.Local.Where(x => !x.IsDeleted)
            .SingleOrDefault(x => x.Id.Equals(id));

        return entity ?? await Set.Where(x => !x.IsDeleted).SingleAsync(x => x.Id.Equals(id), cancellationToken);
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate)
    {
        return GetQueryableIncludingDeleted(predicate).Where(x => !x.IsDeleted);
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetQueryableIncludingDeleted(Expression<Func<TEntity, bool>> predicate)
    {
        return Set.Where(predicate).AsQueryable();
    }
}