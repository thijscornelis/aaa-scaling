using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Module.Users.Domain.Abstractions;
using Module.Users.Domain.Entities;
using Shared.Contracts.Identifiers;

namespace Module.Users.Application.Commands;

public record CreateUser(UserId UserId, string Firstname, string Lastname) : Command<CreateUser.Result>
{
    public record Result : ExecutionResult
    {
        public Result()
        {
            UserId = UserId.Empty;
        }

        public Result(UserId userId)
        {
            UserId = userId;
        }

        public UserId UserId { get; init; }
    }

    public class Handler : CommandHandler<CreateUser, Result>
    {
        private readonly IUserRepository _repository;

        public Handler(IUserRepository repository)
        {
            _repository = repository;
        }

        public override async Task<Result> Handle(CreateUser command, CancellationToken cancellationToken)
        {
            var user = new User(command.UserId, command.Firstname, command.Lastname);
            await _repository.AddAsync(user, cancellationToken);
            return new Result(user.Id);
        }
    }
}