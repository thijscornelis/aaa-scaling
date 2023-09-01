using Foundation.Core.DependencyInjection;
using Jobs.Domain.Abstractions;
using Jobs.Infrastructure.EntityFramework;
using Jobs.Infrastructure.EntityFramework.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Infrastructure;

public class Module : IModule
{
    /// <inheritdoc />
    public IServiceCollection RegisterServices(IServiceCollection services)
    {
        services.WithEntityFramework();

        services.AddDbContext<DbContext, JobsDbContext>((sp, o) =>
        {
            var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Jobs");
            o.UseSqlServer(connectionString);
        });
        services.AddTransient<IJobRepository, JobRepository>();

        return services;
    }

    /// <inheritdoc />
    public IApplicationBuilder ConfigureApplication(IApplicationBuilder app) => app;
}