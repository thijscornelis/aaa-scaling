using Foundation.Core.Abstractions;

namespace Foundation.Core.CQRS.Design;

public interface IQueryExecutor
{
    Task<TResponse> ExecuteAsync<TResponse>(Query<TResponse> query, CancellationToken cancellationToken = default)
        where TResponse : ExecutionResult, new();
}