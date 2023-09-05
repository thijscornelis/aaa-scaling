using Foundation.Core.Abstractions;
using Jobs.Domain.Entities.Identifiers;
using Jobs.Domain.ValueObjects;

namespace Jobs.Domain.Entities;

public class Job : EntityBase<JobId>
{
    /// <inheritdoc />
    protected internal Job()
    {
    }

    /// <inheritdoc />
    public Job(JobId id) : base(id)
    {
    }

    public JobStatus Status { get; private set; } = JobStatus.Created;

    public void SetCompleted()
    {
        Status = JobStatus.Completed;
    }

    public async Task<double> Execute(CancellationToken cancellationToken)
    {
        var random = new Random();
        var seconds = random.Next(3, 30);
        for (var i = 0; i < seconds; i++) await Task.Delay(1000, cancellationToken);
        SetCompleted();
        return seconds;
    }
}