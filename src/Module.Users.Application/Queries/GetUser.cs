using Foundation.Core.Abstractions;
using Foundation.Core.CQRS;
using Module.Users.Application.Contracts;
using Module.Users.Domain.Entities;
using Shared.Contracts.Identifiers;

namespace Module.Users.Application.Queries;

public record GetUser(UserId UserId) : Query<GetUser.Result>
{
    public record Result : ExecutionResult
    {
        public UserDto? UserDto { get; init; }
    }

    public class Handler : QueryHandler<GetUser, Result>
    {
        private readonly IReadOnlyRepository<User, UserId> _repository;

        public Handler(IReadOnlyRepository<User, UserId> repository)
        {
            _repository = repository;
        }

        public override async Task<Result> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.UserId, cancellationToken);
            return new Result() { UserDto = new UserDto(user.Id, $"{user.Firstname} {user.Lastname}") };
        }
    }
}