using MediatR;

namespace Foundation.Core.Abstractions;

public abstract record Query<TQueryResult> : IRequest<TQueryResult>
    where TQueryResult : ExecutionResult;