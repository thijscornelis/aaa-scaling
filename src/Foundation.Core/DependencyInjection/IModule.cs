using Foundation.Core.Abstractions;
using Foundation.Core.CQRS.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Foundation.Core.DependencyInjection;

public interface IModule
{
    IServiceCollection RegisterServices(IServiceCollection collection);
}

public interface IFacade<TModule>
    where TModule : IModule, new()
{
    Task<TResponse> ExecuteAsync<TResponse>(Command<TResponse> request, CancellationToken cancellationToken)
        where TResponse : ExecutionResult, new();

    Task<TResponse> ExecuteAsync<TResponse>(Query<TResponse> request, CancellationToken cancellationToken)
        where TResponse : ExecutionResult, new();
}

public class Facade<TModule> : IFacade<TModule>
    where TModule : IModule, new()
{
    private readonly IServiceProvider _serviceProvider;

    public Facade(IServiceCollection rootServiceCollection, TModule module)
    {
        var builder = new ServiceCollection();
        foreach (var descriptor in rootServiceCollection.ToList())
        {
            builder.Add(descriptor);
        }
        module.RegisterServices(builder);
        _serviceProvider = builder.BuildServiceProvider();
    }

    public async Task<TResponse> ExecuteAsync<TResponse>(Command<TResponse> request,
        CancellationToken cancellationToken)
        where TResponse : ExecutionResult, new()
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var commandExecutor = _serviceProvider.GetRequiredService<ICommandExecutor>();
        return await commandExecutor.ExecuteAsync(request, cancellationToken);
    }

    public async Task<TResponse> ExecuteAsync<TResponse>(Query<TResponse> request, CancellationToken cancellationToken)
        where TResponse : ExecutionResult, new()
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var commandExecutor = _serviceProvider.GetRequiredService<IQueryExecutor>();
        return await commandExecutor.ExecuteAsync(request, cancellationToken);
    }
}