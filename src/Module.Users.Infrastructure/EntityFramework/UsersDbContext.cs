using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Identifiers;

namespace Module.Users.Infrastructure.EntityFramework;

public class UsersDbContext : DbContext
{
    /// <inheritdoc />
    public UsersDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(o => o.MigrationsHistoryTable("EfCoreMigrations", "users"));
        base.OnConfiguring(optionsBuilder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<UserId>().HaveConversion<UserId.EfCoreValueConverter>();
        base.ConfigureConventions(configurationBuilder);
    }
}