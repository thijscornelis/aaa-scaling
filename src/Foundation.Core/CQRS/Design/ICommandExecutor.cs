using Foundation.Core.Abstractions;

namespace Foundation.Core.CQRS.Design;

public interface ICommandExecutor
{
    Task<TResponse> ExecuteAsync<TResponse>(Command<TResponse> command, CancellationToken cancellationToken = default)
        where TResponse : ExecutionResult, new();
}