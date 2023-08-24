using Module.Jobs.Domain.Entities.Identifiers;
using Shared.Core.Abstractions;

namespace Module.Jobs.Domain.Entities;

public class Job : EntityBase<JobId>
{
    public Job() : base(JobId.New())
    {
    }

    public void SetComplete()
    {
        Completed = true;
        CompletedAt = DateTimeOffset.UtcNow;
    }

    public bool Completed { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
}