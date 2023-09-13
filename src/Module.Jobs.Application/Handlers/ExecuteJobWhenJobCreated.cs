using Foundation.Core.CQRS;
using Module.Jobs.Domain.Abstractions;
using Module.Jobs.Domain.Events;

namespace Module.Jobs.Application.Handlers;

internal class ExecuteJobWhenJobCreated : DomainEventHandler<JobCreated>
{
    private readonly IJobRepository _jobRepository;

    public ExecuteJobWhenJobCreated(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public override async Task Handle(JobCreated @event, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetById(@event.JobId, cancellationToken);
        await job.Execute(cancellationToken);
    }
}