using Jobs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobs.Infrastructure.EntityFramework.Configurations;

internal class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithMany(x => x.Jobs);
        builder.OwnsOne(x => x.Status);
    }
}