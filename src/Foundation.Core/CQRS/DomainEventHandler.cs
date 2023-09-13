using Foundation.Core.Abstractions;
using MediatR;

namespace Foundation.Core.CQRS;

public abstract class DomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
    public abstract Task Handle(TEvent @event, CancellationToken cancellationToken);
}