namespace Foundation.Core.Abstractions;

public interface IUnitOfWork
{
    Task BeginAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
    Task PersistAsync(CancellationToken cancellationToken);
}