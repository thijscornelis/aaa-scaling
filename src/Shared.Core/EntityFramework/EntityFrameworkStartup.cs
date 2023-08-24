using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core.EntityFramework;

public static class EntityFrameworkStartup
{
    public static IServiceCollection WithEntityFramework<TContext>(this IServiceCollection services, string connectionStringName)
        where TContext : DbContext =>
        services.AddDbContext<TContext>((sp, x) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(connectionStringName);
            x.UseSqlServer(connectionString);
        });
}