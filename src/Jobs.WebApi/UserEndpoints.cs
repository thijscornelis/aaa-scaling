using Foundation.Core.CQRS.Design;
using Jobs.Application.Commands;
using Jobs.Domain.Entities.Identifiers;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.WebApi;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("users", Create)
            .WithOpenApi()
            .WithName("Create")
            .WithDisplayName("Create")
            .WithDescription("Create a user")
            .WithTags("Users");
        return builder;
    }

    private static async Task<CreateUser.Result> Create([FromBody] CreateUserModel model,
        [FromServices] ICommandExecutor executor, CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new CreateUser(UserId.New(), model.Firstname, model.Lastname),
            cancellationToken);
    }
}

public record CreateUserModel(string Firstname, string Lastname);