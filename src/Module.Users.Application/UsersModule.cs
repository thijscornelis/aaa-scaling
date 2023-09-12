using Foundation.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Users.Domain.Abstractions;
using Module.Users.Infrastructure.EntityFramework;
using Module.Users.Infrastructure.EntityFramework.Repositories;

namespace Module.Users.Application;

public class UsersModule : IModule
{
    public IServiceCollection RegisterServices(IServiceCollection collection)
    {
        collection.AddTransient<IUserRepository, UserRepository>();
        collection.WithCommandAndQueryResponsibilitySegregation(GetType().Assembly);
        collection.WithEntityFramework(x =>
        {
            x.AddDbContext<DbContext, UsersDbContext>((sp, o) =>
            {
                var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("SQL");
                o.UseSqlServer(connectionString);
            });
        });
        return collection;
    }
}