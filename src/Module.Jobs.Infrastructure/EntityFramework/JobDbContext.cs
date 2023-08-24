using Microsoft.EntityFrameworkCore;

namespace Module.Jobs.Infrastructure.EntityFramework;

public class JobDbContext : DbContext
{
    /// <inheritdoc />
    public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
    {
    }
}