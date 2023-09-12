using Foundation.Core.TypedIdentifiers;
using StronglyTypedIds;

[assembly:
    StronglyTypedIdDefaults(
        converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter,
        backingType: StronglyTypedIdBackingType.Guid)]

namespace Shared.Contracts.Identifiers;

[StronglyTypedId]
public readonly partial struct JobId : ITypedId<JobId>
{
}