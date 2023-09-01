using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Core.DependencyInjection;

public interface IModule
{
    IServiceCollection RegisterServices(IServiceCollection services);
    IApplicationBuilder ConfigureApplication(IApplicationBuilder app);
}