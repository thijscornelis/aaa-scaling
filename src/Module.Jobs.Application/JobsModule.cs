using Foundation.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Jobs.Domain.Abstractions;
using Module.Jobs.Infrastructure.EntityFramework;
using Module.Jobs.Infrastructure.EntityFramework.Repositories;

namespace Module.Jobs.Application;

public class JobsModule :IModule
{
    public IServiceCollection RegisterServices(IServiceCollection collection)
    {
        collection.AddTransient<IJobRepository, JobRepository>();
        collection.WithCommandAndQueryResponsibilitySegregation(GetType().Assembly);
        collection.WithEntityFramework(x =>
        {
            x.AddDbContext<DbContext, JobsDbContext>((sp, o) =>
            {
                var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("SQL");
                o.UseSqlServer(connectionString);
            });
        });
        return collection;
    }
}