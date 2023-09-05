using System.Reflection;
using System.Text.Json.Serialization;
using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Foundation.Core.CQRS.Design;
using Foundation.Core.EntityFramework;
using Foundation.Core.Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Core.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection WithEntityFramework(this IServiceCollection services,
        Action<IServiceCollection> registerDbContext)
    {
        services.AddTransient(typeof(IReadOnlyRepository<,>), typeof(EntityFrameworkReadOnlyRepository<,>));
        services.AddTransient(typeof(IRepository<,>), typeof(EntityFrameworkRepository<,>));
        services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
        registerDbContext.Invoke(services);
        return services;
    }

    public static IServiceCollection WithCommandAndQueryResponsibilitySegregation(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddTransient<ICommandExecutor, CommandExecutor>();
        services.AddTransient<IQueryExecutor, QueryExecutor>();
        return services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(assemblies);
            x.AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(StopwatchPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(UnitOfWorkPipelineBehaviour<,>));
        });
    }
}