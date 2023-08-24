using StronglyTypedIds;

[assembly: StronglyTypedIdDefaults(converters: StronglyTypedIdConverter.SystemTextJson)]

namespace Module.Jobs.Domain.Entities.Identifiers;

[StronglyTypedId]
public partial struct JobId
{
}