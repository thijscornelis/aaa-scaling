using Foundation.Core.CQRS;
using Microsoft.AspNetCore.SignalR;
using Module.Jobs.Application.Hubs;
using Module.Jobs.Domain.Events;

namespace Module.Jobs.Application.Handlers;

internal class RealtimeNotificationOnJobCompleted : DomainEventHandler<JobCompleted>
{
    private readonly IHubContext<JobNotificationHub, IJobNotificationHub> _hub;

    public RealtimeNotificationOnJobCompleted(IHubContext<JobNotificationHub, IJobNotificationHub> hub)
    {
        _hub = hub;
    }

    public override async Task Handle(JobCompleted @event, CancellationToken cancellationToken) => await _hub.Clients.All.OnJobCompleted(@event);
}