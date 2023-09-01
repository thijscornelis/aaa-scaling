using Foundation.Core.EntityFramework;
using Jobs.Domain.Entities.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.EntityFramework;

internal class JobsDbContext : EntityFrameworkDbContext
{
    /// <inheritdoc />
    public JobsDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<JobId>().HaveConversion<JobId.EfCoreValueConverter>();
        base.ConfigureConventions(configurationBuilder);
    }
}