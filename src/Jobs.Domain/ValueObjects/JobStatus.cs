using Foundation.Core.Abstractions;

namespace Jobs.Domain.ValueObjects;

public record JobStatus(string Name) : ValueObject
{
    public static JobStatus Created => new("Created");
    public static JobStatus Completed => new("Completed");
}