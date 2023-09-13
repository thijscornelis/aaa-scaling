using Foundation.Core.Abstractions;
using Foundation.Core.TypedIdentifiers;
using Shared.Contracts.Identifiers;

namespace Module.Jobs.Domain.Events;

public record JobCreated(DomainEventId DomainEventId, JobId JobId) : IDomainEvent;
public record JobCompleted(DomainEventId DomainEventId, JobId JobId, TimeSpan TimeSpan) : IDomainEvent;