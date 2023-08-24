using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core.DependencyInjection;

public interface IModule
{
    void ConfigureServices(IServiceCollection services);

    void Configure(IApplicationBuilder app);
}