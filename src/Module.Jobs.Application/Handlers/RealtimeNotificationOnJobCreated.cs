using Foundation.Core.CQRS;
using Microsoft.AspNetCore.SignalR;
using Module.Jobs.Application.Hubs;
using Module.Jobs.Domain.Events;

namespace Module.Jobs.Application.Handlers;

internal class RealtimeNotificationOnJobCreated : DomainEventHandler<JobCreated>
{
    private readonly IHubContext<JobNotificationHub, IJobNotificationHub> _hub;

    public RealtimeNotificationOnJobCreated(IHubContext<JobNotificationHub, IJobNotificationHub> hub)
    {
        _hub = hub;
    }

    public override async Task Handle(JobCreated @event, CancellationToken cancellationToken) => await _hub.Clients.All.OnJobCreated(@event);
}