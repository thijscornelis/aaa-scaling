
using Foundation.Core.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Module.Jobs.Application;
using Module.Jobs.Application.Hubs;
using Module.Jobs.Domain.Events;
using Module.Jobs.Infrastructure.EntityFramework;
using Module.Users.Application;
using Module.Users.Infrastructure.EntityFramework;
using Scaling.API.Endpoints;
using Shared.Contracts.Identifiers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Scaling.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSignalR();

            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(x =>
                {
                    x.OrderActionsBy(a => a.HttpMethod);
                    x.CustomSchemaIds(type => type.ToString().Replace('+', '-'));
                    x.MapTypedIdentifier<JobId>();
                    x.MapTypedIdentifier<UserId>();
                })
                .AddModule<UsersModule>()
                .AddModule<JobsModule>()
                .AddHostedService<HostedSignalRListener>();
                

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapJobEndpoints();
            app.MapUserEndpoints();
            app.MapHub<JobNotificationHub>("signalr/jobs");
            app.Run();
        }
    }


    public class UserDbContextDesignTimeFactory : DbContextDesignTimeFactory<UsersDbContext>
    {
    }
    public class JobsDbContextDesignTimeFactory : DbContextDesignTimeFactory<JobsDbContext>
    {
    }

    public abstract class DbContextDesignTimeFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true)
                .AddUserSecrets(GetType().Assembly, true)
                .Build();

            var builder = new DbContextOptionsBuilder<TContext>();
            var connectionString = configuration.GetConnectionString("Sql");
            builder.UseSqlServer(connectionString);
            return (TContext) Activator.CreateInstance(typeof(TContext),builder.Options)!;
        }
    }


    public class HostedSignalRListener : IHostedService
    {
        private HubConnection? _connection;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7086/signalr/jobs")
                .WithAutomaticReconnect()
                .Build();

            _connection.On(nameof(IJobNotificationHub.OnJobCreated), (JobCreated @event) =>
            {
                System.Console.WriteLine($"Job with {@event.JobId} was created");
                return Task.CompletedTask;
            });
            _connection.On(nameof(IJobNotificationHub.OnJobCompleted), (JobCompleted @event) =>
            {
                System.Console.WriteLine($"Job with {@event.JobId} was completed after {@event.TimeSpan}");
                return Task.CompletedTask;
            });
            return _connection.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_connection != null)
            {
                await _connection.StopAsync(cancellationToken);
            }
        }
    }
}