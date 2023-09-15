using Foundation.Core.Abstractions;
using Foundation.Core.CQRS.Design;
using MediatR;

namespace Foundation.Core.CQRS;

public class DomainEventExecutor : IDomainEventExecutor
{
    private readonly IMediator _mediator;

    public DomainEventExecutor(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <inheritdoc />
    public Task ExecuteAsync(CancellationToken cancellationToken = default, params IDomainEvent[] events)
    {
        var tasks = events.Select(x => _mediator.Publish(x, cancellationToken));
        return Task.WhenAll(tasks);
    }
}