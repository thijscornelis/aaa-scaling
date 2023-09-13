using Foundation.Core.Abstractions;
using Foundation.Core.CQRS.Design;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Foundation.Core.Mediator;

internal class PublishDomainEventsPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : Command<TResponse>
    where TResponse : ExecutionResult, new()
{
    private readonly ILogger<PublishDomainEventsPipelineBehaviour<TRequest, TResponse>> _logger;
    private readonly DbContext _dbContext;
    private readonly IDomainEventExecutor _domainEventExecutor;
    public PublishDomainEventsPipelineBehaviour(ILogger<PublishDomainEventsPipelineBehaviour<TRequest, TResponse>> logger, DbContext dbContext, IDomainEventExecutor domainEventExecutor)
    {
        _logger = logger;
        _dbContext = dbContext;
        _domainEventExecutor = domainEventExecutor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = await next();
        
        var events = GetDomainEvents();
        do
        {
            var tasks = events.Select(@event => _domainEventExecutor.ExecuteAsync(@event, cancellationToken));
            await Task.WhenAll(tasks);
            events = GetDomainEvents();
        } while (events.Any());

        return result;
    }

    private IDomainEvent[] GetDomainEvents()
    {
        return _dbContext.ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added)
            .Select(x => x.Entity)
            .SelectMany(x =>
            {
                var events = x.DomainEvents.ToList();
                x.ClearDomainEvents();
                return events;
            })
            .ToArray();
    }
}