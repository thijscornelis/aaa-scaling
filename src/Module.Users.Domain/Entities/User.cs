using Foundation.Core.Abstractions;
using Shared.Contracts.Identifiers;

namespace Module.Users.Domain.Entities;

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

    public string Firstname { get; private set; } = null!;
    public string Lastname { get; private set; } = null!;
}