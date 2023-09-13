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
    public Task ExecuteAsync(IDomainEvent query, CancellationToken cancellationToken = default) => _mediator.Publish(query, cancellationToken);
}