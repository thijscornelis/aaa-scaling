using System.Reflection;
using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Foundation.Core.CQRS.Design;
using Foundation.Core.EntityFramework;
using Foundation.Core.Mediator;
using Foundation.Core.TypedIdentifiers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Foundation.Core.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static SwaggerGenOptions MapTypedIdentifier<TType>(this SwaggerGenOptions options)
        where TType : ITypedId<TType>
    {
        options.MapType(typeof(TType), () => new OpenApiSchema()
        {
            Type = nameof(Guid),
            Example = new OpenApiString(Guid.NewGuid().ToString())
        });

        return options;
    }
    public static IServiceCollection WithEntityFramework(this IServiceCollection services,
        Action<IServiceCollection> registerDbContext)
    {
        services.AddTransient(typeof(IReadOnlyRepository<,>), typeof(EntityFrameworkReadOnlyRepository<,>));
        services.AddTransient(typeof(IRepository<,>), typeof(EntityFrameworkRepository<,>));
        services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
        registerDbContext.Invoke(services);
        return services;
    }

    public static IServiceCollection AddModule<TModule>(this IServiceCollection serviceCollection)
        where TModule : class, IModule
    {
        serviceCollection.AddTransient<TModule>();
        serviceCollection.AddSingleton(serviceCollection);
        serviceCollection.AddScoped(typeof(IFacade<>), typeof(Facade<>));
        return serviceCollection;
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