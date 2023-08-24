using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Jobs.Domain.Entities;

namespace Module.Jobs.Infrastructure.EntityFramework.Configurations;

internal class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);
    }
}