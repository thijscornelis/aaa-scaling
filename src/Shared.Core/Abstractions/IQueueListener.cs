namespace Shared.Core.Abstractions;

public interface IQueueListener
{
    Task ProcessMessageAsync(CancellationToken cancellationToken);
}