using MediatR;
using Shared.Core.Abstractions;

namespace Shared.Core.Commands;

public abstract class CommandHandler<TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult>
    where TCommand : Command<TCommandResult>
    where TCommandResult : ExecutionResult, new()
{
    /// <inheritdoc />
    public abstract Task<TCommandResult> Handle(TCommand request, CancellationToken cancellationToken);
}