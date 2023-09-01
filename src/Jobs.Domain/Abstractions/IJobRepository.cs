using Foundation.Core.Abstractions;
using Jobs.Domain.Entities;
using Jobs.Domain.Entities.Identifiers;

namespace Jobs.Domain.Abstractions;

public interface IJobRepository : IRepository<Job, JobId>
{
}