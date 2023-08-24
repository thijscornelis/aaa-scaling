using Module.Jobs.Domain.Entities;
using Module.Jobs.Domain.Entities.Identifiers;
using Shared.Core.Abstractions;

namespace Module.Jobs.Domain.Abstractions;

public interface IJobRepository : IRepository<Job, JobId>
{
}