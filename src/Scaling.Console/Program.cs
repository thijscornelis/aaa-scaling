using Microsoft.AspNetCore.SignalR.Client;
using Module.Jobs.Application.Hubs;
using Module.Jobs.Domain.Events;

namespace Scaling.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7086/signalr/jobs")
                .WithAutomaticReconnect()
                .Build();

            connection.On(nameof(IJobNotificationHub.OnJobCreated), (JobCreated @event) =>
            {
                System.Console.WriteLine($"Job with {@event.JobId} was created");
                return Task.CompletedTask;
            });
            connection.On(nameof(IJobNotificationHub.OnJobCompleted), (JobCompleted @event) =>
            {
                System.Console.WriteLine($"Job with {@event.JobId} was completed after {@event.TimeSpan}");
                return Task.CompletedTask;
            });

            await connection.StartAsync();

            System.Console.ReadLine();
        }
    }
}