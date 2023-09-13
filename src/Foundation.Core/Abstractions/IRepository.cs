using Foundation.Core.TypedIdentifiers;
using MediatR;

namespace Foundation.Core.Abstractions;

public interface IRepository<TEntity, in TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : EntityBase<TKey>
    where TKey : struct
{
    Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}

public interface IDomainEvent : INotification
{
    public DomainEventId DomainEventId { get; init; }
}