using Foundation.Core.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Foundation.Core.Mediator;

internal class UnitOfWorkPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : Command<TResponse>
    where TResponse : ExecutionResult, new()
{
    private readonly ILogger<UnitOfWorkPipelineBehaviour<TRequest, TResponse>> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkPipelineBehaviour(ILogger<UnitOfWorkPipelineBehaviour<TRequest, TResponse>> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse response;
        try
        {
            _logger.LogTrace("Wrapping {0} in a Unit of Work", typeof(TRequest).Name);
            await _unitOfWork.BeginAsync(cancellationToken);
            response = await next();
        }
        catch (Exception e)
        {
            _logger.LogTrace("Rolling back transaction for {0} because of exception {1}", typeof(TRequest).Name,
                e.GetBaseException().GetType().Name);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }

        _logger.LogTrace("Committing transaction for {0}", typeof(TRequest).Name);
        await _unitOfWork.CommitAsync(cancellationToken);
        _logger.LogTrace("Persisting transaction for {0}", typeof(TRequest).Name);
        await _unitOfWork.PersistAsync(cancellationToken);

        return response;
    }
}