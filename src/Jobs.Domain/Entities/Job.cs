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
}