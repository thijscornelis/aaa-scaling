using Foundation.Core.Abstractions;

namespace Foundation.Core.CQRS.Design;

public interface IDomainEventExecutor
{
    Task ExecuteAsync(IDomainEvent query, CancellationToken cancellationToken = default);
}