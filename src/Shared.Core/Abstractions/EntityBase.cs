namespace Shared.Core.Abstractions;

public class EntityBase<TKey>
    where TKey : struct
{

    protected EntityBase(TKey id)
    {
        Id = id;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    internal void SetModified() => UpdatedAt = DateTimeOffset.UtcNow;

    public TKey Id { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    internal void Delete() => IsDeleted = true;
}