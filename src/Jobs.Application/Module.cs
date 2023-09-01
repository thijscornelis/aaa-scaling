using Foundation.Core.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Application;

public class Module : IModule
{
    /// <inheritdoc />
    public IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.WithMediator(GetType().Assembly);
        return services;
    }

    /// <inheritdoc />
    public IApplicationBuilder ConfigureApplication(IApplicationBuilder app) => app;
}