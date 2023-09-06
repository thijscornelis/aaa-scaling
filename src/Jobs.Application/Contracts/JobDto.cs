using Jobs.Domain.Entities.Identifiers;
using Jobs.Domain.ValueObjects;

namespace Jobs.Application.Contracts;

public record JobDto(JobId Id, JobStatus Status, DateTimeOffset CreatedOn, DateTimeOffset ModifiedOn);