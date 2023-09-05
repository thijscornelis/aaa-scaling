using Foundation.Core.Abstractions;
using Foundation.Core.CQRS.Design;
using MediatR;

namespace Foundation.Core.CQRS;

public class CommandExecutor : ICommandExecutor
{
    private readonly IMediator _mediator;

    public CommandExecutor(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <inheritdoc />
    public Task<TResponse> ExecuteAsync<TResponse>(Command<TResponse> command,
        CancellationToken cancellationToken = default) where TResponse : ExecutionResult, new()
    {
        return _mediator.Send(command, cancellationToken);
    }
}