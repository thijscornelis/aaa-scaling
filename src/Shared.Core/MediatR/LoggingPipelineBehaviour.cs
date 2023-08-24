using MediatR;
using Microsoft.Extensions.Logging;

namespace Shared.Core.MediatR;

internal class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;

    public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Starting {0}", typeof(TRequest).Name);
        var response = await next();
        _logger.LogTrace("Finishing {0}", typeof(TRequest).Name);
        return response;
    }
}