using Foundation.Core.Abstractions;
using Module.Jobs.Domain.Entities;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Domain.Abstractions;

public interface IJobRepository : IRepository<Job, JobId>
{
}