using MediatR;
using Shared.Core.Abstractions;

namespace Shared.Core.MediatR;

internal class ExceptionHandlingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
    where TResponse : ExecutionResult, new()
{
    /// <inheritdoc />
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return next();
        }
        catch (Exception e)
        {
            var response = new TResponse();
            response.SetException(e);
            return Task.FromResult(response);
        }
    }
}