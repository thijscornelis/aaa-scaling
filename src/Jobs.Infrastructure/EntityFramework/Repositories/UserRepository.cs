using Foundation.Core.EntityFramework;
using Jobs.Domain.Abstractions;
using Jobs.Domain.Entities;
using Jobs.Domain.Entities.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.EntityFramework.Repositories;

public class UserRepository : EntityFrameworkRepository<User, UserId>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}