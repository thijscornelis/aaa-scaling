using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Foundation.Core.Abstractions;
using Foundation.Core.EntityFramework;
using Foundation.Core.Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Core.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection WithTypedIdentifiers(this IServiceCollection services, params JsonConverter[] converters)
    {
      
        return services;
    }

    public static IServiceCollection WithEntityFramework(this IServiceCollection services)
    {
        services.AddTransient(typeof(IReadOnlyRepository<,>), typeof(EntityFrameworkReadOnlyRepository<,>));
        services.AddTransient(typeof(IRepository<,>), typeof(EntityFrameworkRepository<,>));
        services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
        return services;
    }
    public static IServiceCollection WithMediator(this IServiceCollection services, params Assembly[] assemblies) =>
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(assemblies);
            x.AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(StopwatchPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(UnitOfWorkPipelineBehaviour<,>));
        });

    public static IServiceCollection RegisterModules(this IServiceCollection services, params IModule[] modules)
    {
        foreach (var module in modules)
        {
            services = module.RegisterServices(services);
        }

        return services;
    }

    public static IApplicationBuilder ConfigureModule(this IApplicationBuilder applicationBuilder, params IModule[] modules)
    {
        foreach (var module in modules)
        {
            applicationBuilder = module.ConfigureApplication(applicationBuilder);
        }

        return applicationBuilder;
    }
}