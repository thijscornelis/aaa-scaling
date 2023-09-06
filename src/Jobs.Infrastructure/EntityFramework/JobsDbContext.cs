using Jobs.Domain.Entities.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.EntityFramework;

public class JobsDbContext : DbContext
{
    /// <inheritdoc />
    public JobsDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<JobId>().HaveConversion<JobId.EfCoreValueConverter>();
        configurationBuilder.Properties<UserId>().HaveConversion<UserId.EfCoreValueConverter>();
        base.ConfigureConventions(configurationBuilder);
    }
}