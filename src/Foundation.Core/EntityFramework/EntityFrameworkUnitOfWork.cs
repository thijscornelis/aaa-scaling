using Foundation.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Foundation.Core.EntityFramework;

public class EntityFrameworkUnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private IDbContextTransaction? _transaction;
    public EntityFrameworkUnitOfWork(DbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task BeginAsync(CancellationToken cancellationToken)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        if (_transaction == null) return;

        foreach (var entity in _context.ChangeTracker
                     .Entries<EntityBase>()
                     .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added)
                     .Select(x => x.Entity))
        {
            entity.SetModified();
        }

        await _transaction.CommitAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        if(_transaction == null) return;
        await _transaction.RollbackAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task PersistAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
}