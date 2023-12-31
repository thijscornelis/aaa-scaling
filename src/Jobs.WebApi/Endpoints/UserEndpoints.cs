﻿using Foundation.Core.CQRS.Design;
using Jobs.Application.Commands;
using Jobs.Domain.Entities.Identifiers;
using Jobs.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.WebApi.Endpoints;

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
        [FromServices] ICommandExecutor executor, CancellationToken cancellationToken)
    {
        var result = await executor.ExecuteAsync(new CreateUser(UserId.New(), model.Firstname, model.Lastname),
            cancellationToken);
        return result.ToHttpResult();
    }
}