using Shared.Core.Abstractions;

namespace Shared.Core.RabbitMQ;

internal class RabbitMQExchangePublisher : ITopicPublisher
{
    /// <inheritdoc />
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc />
    public Task PublishAsync<TEvent>(CancellationToken cancellationToken, params TEvent[] events) => throw new NotImplementedException();
}