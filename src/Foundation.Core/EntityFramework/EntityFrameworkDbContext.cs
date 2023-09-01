using Microsoft.EntityFrameworkCore;

namespace Foundation.Core.EntityFramework;

public class EntityFrameworkDbContext : DbContext
{
    /// <inheritdoc />
    public EntityFrameworkDbContext(DbContextOptions options) : base(options)
    {
    }
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}