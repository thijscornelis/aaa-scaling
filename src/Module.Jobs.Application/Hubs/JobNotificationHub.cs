using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Module.Jobs.Domain.Events;

namespace Module.Jobs.Application.Hubs;

public interface IJobNotificationHub
{
    Task OnJobCreated(JobCreated @event);
    Task OnJobCompleted(JobCompleted @event);
}

public class JobNotificationHub : Hub<IJobNotificationHub>
{
    private readonly ILogger<JobNotificationHub> _logger;

    public JobNotificationHub(ILogger<JobNotificationHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("User {0} connected to {1}", Context.ConnectionId, nameof(JobNotificationHub));
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("User {0} disconnected from {1}", Context.ConnectionId, nameof(JobNotificationHub));
        return base.OnDisconnectedAsync(exception);
    }
}