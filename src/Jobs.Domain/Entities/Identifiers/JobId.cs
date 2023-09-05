using Foundation.Core.TypedIdentifiers;
using StronglyTypedIds;

[assembly:
    StronglyTypedIdDefaults(
        converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter,
        backingType: StronglyTypedIdBackingType.Guid)]

namespace Jobs.Domain.Entities.Identifiers;

[StronglyTypedId]
public readonly partial struct JobId : ITypedId<JobId>
{
}