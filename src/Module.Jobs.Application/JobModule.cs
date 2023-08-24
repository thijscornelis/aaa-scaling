using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.DependencyInjection;

namespace Module.Jobs.Application;

public class JobModule : IModule
{
    /// <inheritdoc />
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Configure(IApplicationBuilder app)
    {
        throw new NotImplementedException();
    }
}