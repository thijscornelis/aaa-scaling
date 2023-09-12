using Foundation.Core.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Module.Users.Domain.Abstractions;
using Module.Users.Domain.Entities;
using Shared.Contracts.Identifiers;

namespace Module.Users.Infrastructure.EntityFramework.Repositories;

public class UserRepository : EntityFrameworkRepository<User, UserId>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}