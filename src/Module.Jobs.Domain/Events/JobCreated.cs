using Foundation.Core.Abstractions;
using Foundation.Core.TypedIdentifiers;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Domain.Events;

public record JobCreated : IDomainEvent
{
    public required DomainEventId DomainEventId { get; init; }
    public required JobId JobId { get; init; }
}

public record JobCompleted : IDomainEvent
{
    public required DomainEventId DomainEventId { get; init; } 
    public required JobId JobId { get; init; } 
    public required TimeSpan TimeSpan { get; init; }
}