using Foundation.Core.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Module.Users.Application;
using Module.Users.Application.Commands;
using Scaling.API.Models;
using Shared.Contracts.Identifiers;

namespace Scaling.API.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("users", Create)
            .WithOpenApi()
            .WithDisplayName("Create")
            .WithDescription("Create a user")
            .WithTags("Users");
        return builder;
    }

    private static async Task<IResult> Create([FromBody] CreateUserModel model,
        [FromServices] IFacade<UsersModule> executor, CancellationToken cancellationToken)
    {
        var result = await executor.ExecuteAsync(new CreateUser(UserId.New(), model.Firstname, model.Lastname),
            cancellationToken);
        return result.ToHttpResult();
    }
}