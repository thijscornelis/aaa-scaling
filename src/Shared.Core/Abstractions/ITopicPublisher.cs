namespace Shared.Core.Abstractions;

public interface ITopicPublisher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken);
    Task PublishAsync<TEvent>(CancellationToken cancellationToken, params TEvent[] events);

}