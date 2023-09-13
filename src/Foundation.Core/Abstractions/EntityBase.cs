using System.Text.Json.Serialization;

namespace Foundation.Core.Abstractions;

public abstract class EntityBase
{
    protected EntityBase()
    {
        CreatedOn = DateTimeOffset.UtcNow;
        ModifiedOn = CreatedOn;
    }

    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset ModifiedOn { get; private set; }
    public bool IsDeleted { get; private set; }


    [JsonIgnore] internal HashSet<IDomainEvent> DomainEvents { get; } = new HashSet<IDomainEvent>();
    protected internal void AddDomainEvent(IDomainEvent domainEvent) => DomainEvents.Add(domainEvent);
    protected internal void ClearDomainEvents() => DomainEvents.Clear();

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
    protected bool Equals(EntityBase<TKey> other)
    {
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EntityBase<TKey>)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

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