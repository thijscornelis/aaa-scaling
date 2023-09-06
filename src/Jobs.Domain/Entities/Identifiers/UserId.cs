using Foundation.Core.TypedIdentifiers;
using StronglyTypedIds;

namespace Jobs.Domain.Entities.Identifiers;

[StronglyTypedId]
public readonly partial struct UserId : ITypedId<UserId>
{
}