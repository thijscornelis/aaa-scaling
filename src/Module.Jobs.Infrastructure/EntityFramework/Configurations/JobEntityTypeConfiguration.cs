using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Jobs.Domain.Entities;

namespace Module.Jobs.Infrastructure.EntityFramework.Configurations;

internal class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired();
        builder.OwnsOne(x => x.Status);
    }
}