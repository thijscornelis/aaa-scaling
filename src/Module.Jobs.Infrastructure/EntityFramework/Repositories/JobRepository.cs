using Foundation.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Module.Jobs.Domain.Abstractions;
using Module.Jobs.Domain.Entities;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Infrastructure.EntityFramework.Repositories;

public class JobRepository : EntityFrameworkRepository<Job, JobId>, IJobRepository
{
    /// <inheritdoc />
    public JobRepository(DbContext context) : base(context)
    {
    }
}