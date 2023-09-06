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
    public Job(JobId id, User user) : base(id)
    {
        ArgumentNullException.ThrowIfNull(user);
        SetUser(user);
    }

    public UserId UserId { get; private set; }
    public virtual User User { get; private set; }
    public JobStatus Status { get; private set; } = JobStatus.Created;

    public void SetUser(User user)
    {
        User = user;
        UserId = user.Id;
    }

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