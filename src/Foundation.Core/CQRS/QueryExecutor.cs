using Foundation.Core.Abstractions;
using Foundation.Core.CQRS.Design;
using MediatR;

namespace Foundation.Core.CQRS;

public class QueryExecutor : IQueryExecutor
{
    private readonly IMediator _mediator;

    public QueryExecutor(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <inheritdoc />
    public Task<TResponse> ExecuteAsync<TResponse>(Query<TResponse> query,
        CancellationToken cancellationToken = default) where TResponse : ExecutionResult, new() =>
        _mediator.Send(query, cancellationToken);
}