using StronglyTypedIds;
[assembly:
    StronglyTypedIdDefaults(
        converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter,
        backingType: StronglyTypedIdBackingType.Guid)]

namespace Foundation.Core.TypedIdentifiers;

[StronglyTypedId]
public partial struct DomainEventId
{
}