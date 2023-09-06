using Foundation.Core.Abstractions;
using Jobs.Domain.Entities.Identifiers;

namespace Jobs.Domain.Entities;

public class User : EntityBase<UserId>
{
    /// <inheritdoc />
    protected internal User()
    {
    }

    public User(UserId id, string firstname, string lastname) : base(id)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstname);
        ArgumentException.ThrowIfNullOrEmpty(lastname);

        Firstname = firstname;
        Lastname = lastname;
    }

    public ICollection<Job> Jobs { get; private set; } = new HashSet<Job>();
    public string Firstname { get; private set; } = null!;
    public string Lastname { get; private set; } = null!;
}