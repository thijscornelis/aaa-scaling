using Jobs.Domain.Entities;
using Jobs.Domain.Entities.Identifiers;
using Jobs.Domain.ValueObjects;

namespace Jobs.Application.Contracts;

public record JobDto(JobId Id, JobStatus Status, DateTimeOffset CreatedOn, DateTimeOffset ModifiedOn)
{
    public static explicit operator JobDto(Job job) => new (job.Id, job.Status, job.CreatedOn, job.ModifiedOn);
}
