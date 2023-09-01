using Foundation.Core.EntityFramework;
using Jobs.Domain.Abstractions;
using Jobs.Domain.Entities;
using Jobs.Domain.Entities.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.EntityFramework.Repositories;

internal class JobRepository : EntityFrameworkRepository<Job, JobId>, IJobRepository
{
    /// <inheritdoc />
    public JobRepository(DbContext context) : base(context)
    {
    }
}