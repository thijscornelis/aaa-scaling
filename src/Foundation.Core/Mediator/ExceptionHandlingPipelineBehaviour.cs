using Foundation.Core.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Foundation.Core.Mediator;

internal class ExceptionHandlingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : ExecutionResult, new()
{
    private readonly ILogger<ExceptionHandlingPipelineBehaviour<TRequest, TResponse>> _logger;

    public ExceptionHandlingPipelineBehaviour(ILogger<ExceptionHandlingPipelineBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return next();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Caught exception of type {0} in request pipeline", e.GetBaseException().GetType().Name);
            var response = new TResponse();
            response.SetException(e);
            return Task.FromResult(response);
        }
    }
}