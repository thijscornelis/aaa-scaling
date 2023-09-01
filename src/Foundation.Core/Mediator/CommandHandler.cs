using Foundation.Core.Abstractions;
using MediatR;

namespace Foundation.Core.Mediator;

public abstract class CommandHandler<TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult>
    where TCommand : Command<TCommandResult>
    where TCommandResult : ExecutionResult, new()
{
    /// <inheritdoc />
    public abstract Task<TCommandResult> Handle(TCommand request, CancellationToken cancellationToken);
}