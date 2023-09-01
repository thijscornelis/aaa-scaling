using MediatR;

namespace Foundation.Core.Abstractions;

public abstract record Command<TCommandResult> : IRequest<TCommandResult>
    where TCommandResult : ExecutionResult, new()
{
}