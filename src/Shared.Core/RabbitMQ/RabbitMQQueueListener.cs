using Microsoft.Extensions.Hosting;
using Shared.Core.Abstractions;

namespace Shared.Core.RabbitMQ;

internal class RabbitMQQueueListener : IQueueListener, IHostedService
{
    /// <inheritdoc />
    public Task ProcessMessageAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc />
    public Task StartAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}