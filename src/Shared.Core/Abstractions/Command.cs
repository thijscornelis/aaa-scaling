using MediatR;

namespace Shared.Core.Abstractions;

public abstract class Command<TCommandResult> : IRequest<TCommandResult>
    where TCommandResult : ExecutionResult, new()
{
}