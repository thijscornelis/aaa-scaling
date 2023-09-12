using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Infrastructure.EntityFramework;

public class JobsDbContext : DbContext
{
    /// <inheritdoc />
    public JobsDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(o => o.MigrationsHistoryTable("EfCoreMigrations", "jobs"));
        base.OnConfiguring(optionsBuilder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("jobs");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
    /// <inheritdoc />
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<UserId>().HaveConversion<UserId.EfCoreValueConverter>();
        configurationBuilder.Properties<JobId>().HaveConversion<JobId.EfCoreValueConverter>();
        base.ConfigureConventions(configurationBuilder);
    }
}