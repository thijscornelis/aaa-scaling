using Foundation.Core.Abstractions;

namespace Foundation.Core.CQRS.Design;

public interface IDomainEventExecutor
{
    Task ExecuteAsync(CancellationToken cancellationToken = default, params IDomainEvent[] events);
}