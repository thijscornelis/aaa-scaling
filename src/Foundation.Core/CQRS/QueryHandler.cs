using Foundation.Core.Abstractions;
using MediatR;

namespace Foundation.Core.CQRS;

public abstract class QueryHandler<TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
    where TQuery : Query<TQueryResult>
    where TQueryResult : ExecutionResult, new()
{
    /// <inheritdoc />
    public abstract Task<TQueryResult> Handle(TQuery request, CancellationToken cancellationToken);
}