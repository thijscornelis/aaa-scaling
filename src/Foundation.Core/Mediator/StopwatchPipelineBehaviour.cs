using System.Diagnostics;
using Foundation.Core.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Foundation.Core.Mediator;

internal class StopwatchPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : ExecutionResult
{
    private readonly ILogger<StopwatchPipelineBehaviour<TRequest, TResponse>> _logger;

    public StopwatchPipelineBehaviour(ILogger<StopwatchPipelineBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var response = await next();
        stopwatch.Stop();
        response.SetElapsedTime(stopwatch.Elapsed);
        _logger.LogTrace("{0} finished in {1}", typeof(TRequest).Name, response.ElapsedTime);
        return response;
    }
}