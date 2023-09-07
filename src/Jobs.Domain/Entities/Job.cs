using System.Diagnostics.CodeAnalysis;
using Foundation.Core.Abstractions;
using Jobs.Domain.Entities.Identifiers;
using Jobs.Domain.ValueObjects;

namespace Jobs.Domain.Entities;

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
    public Job(JobId id, User user) : base(id)
    {
        SetCreated();
        SetUser(user);
    }

    public UserId UserId { get; private set; }
    public virtual User User { get; private set; }
    public JobStatus Status { get; private set; }

    [MemberNotNull(nameof(Job.User))]
    public void SetUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        User = user;
        UserId = user.Id;
    }

    [MemberNotNull(nameof(Job.Status))]
    public void SetCompleted()
    {
        Status = JobStatus.Completed;
    }


    [MemberNotNull(nameof(Job.Status))]
    private void SetCreated()
    {
        Status = JobStatus.Created;
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
        SetCompleted();
        return seconds;
    }
}