using Foundation.Core.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Domain;

public class Module : IModule
{
    /// <inheritdoc />
    public IServiceCollection RegisterServices(IServiceCollection services) => services;

    /// <inheritdoc />
    public IApplicationBuilder ConfigureApplication(IApplicationBuilder app) => app;
}