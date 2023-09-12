using Foundation.Core.Abstractions;
using Module.Users.Domain.Entities;
using Shared.Contracts.Identifiers;

namespace Module.Users.Domain.Abstractions;

public interface IUserRepository : IRepository<User, UserId>
{
}