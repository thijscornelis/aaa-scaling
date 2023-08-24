using Microsoft.EntityFrameworkCore;
using Module.Jobs.Domain.Abstractions;
using Module.Jobs.Domain.Entities;
using Module.Jobs.Domain.Entities.Identifiers;
using Shared.Core.EntityFramework;

namespace Module.Jobs.Infrastructure.EntityFramework.Repositories;

public class JobRepository : EntityFrameworkRepository<Job, JobId>,IJobRepository
{
    /// <inheritdoc />
    public JobRepository(DbContext context) : base(context)
    {
    }
}