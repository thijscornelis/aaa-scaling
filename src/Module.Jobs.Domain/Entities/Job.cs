using System.Diagnostics.CodeAnalysis;
using Foundation.Core.Abstractions;
using Foundation.Core.TypedIdentifiers;
using Module.Jobs.Domain.Events;
using Module.Jobs.Domain.ValueObjects;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Domain.Entities;

public class Job : EntityBase<JobId>
{
    /// <inheritdoc />
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected internal Job()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        SetCreated();
    }

    /// <inheritdoc />
    public Job(JobId id, UserId userId) : base(id)
    {
        ArgumentNullException.ThrowIfNull(userId);
        UserId = userId;
        SetCreated();
    }

    public UserId UserId { get; private set; }
    public JobStatus Status { get; private set; }

    [MemberNotNull(nameof(Job.Status))]
    public void SetCompleted(TimeSpan duration)
    {
        Status = JobStatus.Completed;
        AddDomainEvent(new JobCompleted(DomainEventId.New(), Id, duration));
    }

    [MemberNotNull(nameof(Job.Status))]
    private void SetCreated()
    {
        Status = JobStatus.Created;
        AddDomainEvent(new JobCreated(DomainEventId.New(), Id));
    }

    public async Task<double> Execute(CancellationToken cancellationToken)
    {
        // ReSharper disable once CollectionNeverQueried.Local
        var wastedMemory = new List<byte[]>();
        var random = new Random();
        var seconds = random.Next(3, 10);
        for (var i = 0; i < seconds; i++)
        {
            // EatMemory
            byte[] buffer = new byte[262144];
            wastedMemory.Add(buffer);
            await Task.Delay(1000, cancellationToken);
        }
        SetCompleted(TimeSpan.FromSeconds(seconds));
        return seconds;
    }
}