using Shared.Contracts.Identifiers;

namespace Module.Users.Application.Contracts;

public record UserDto(UserId UserId, string UserName);