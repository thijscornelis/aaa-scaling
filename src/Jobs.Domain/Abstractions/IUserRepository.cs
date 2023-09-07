using Foundation.Core.Abstractions;
using Jobs.Domain.Entities;
using Jobs.Domain.Entities.Identifiers;

namespace Jobs.Domain.Abstractions;

public interface IUserRepository : IRepository<User, UserId>
{
}

public interface IDirectoryAccessor
{
    Task<string> GetFileContentAsync(string filename, CancellationToken cancellationToken);
    Task WriteFileContentAsync(string filename, string content, CancellationToken cancellationToken);
}