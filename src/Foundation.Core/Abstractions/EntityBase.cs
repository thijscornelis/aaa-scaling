namespace Foundation.Core.Abstractions;

public abstract class EntityBase
{
    protected EntityBase()
    {
        CreatedOn = DateTimeOffset.UtcNow;
        ModifiedOn = CreatedOn;
    }

    public DateTimeOffset CreatedOn { get; }
    public DateTimeOffset ModifiedOn { get; private set; }
    public bool IsDeleted { get; private set; }

    internal void SetModified()
    {
        ModifiedOn = DateTimeOffset.UtcNow;
    }

    internal void Delete()
    {
        IsDeleted = true;
    }
}

public abstract class EntityBase<TKey> : EntityBase
    where TKey : struct
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityBase{TKey}" /> class.
    /// </summary>
    /// <remarks>Required by ORM</remarks>
    protected EntityBase()
    {
    }

    protected EntityBase(TKey id)
    {
        Id = id;
    }

    public TKey Id { get; private set; }
}