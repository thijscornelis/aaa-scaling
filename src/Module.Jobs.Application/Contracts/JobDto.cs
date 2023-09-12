using Module.Jobs.Domain.ValueObjects;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Application.Contracts;

public record JobDto(JobId Id, JobStatus Status, DateTimeOffset CreatedOn, DateTimeOffset ModifiedOn);