﻿using Foundation.Core.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Module.Jobs.Application;
using Module.Jobs.Application.Commands;
using Module.Jobs.Application.Queries;
using Scaling.API.Models;
using Shared.Contracts.Identifiers;

namespace Scaling.API.Endpoints;

public static class JobEndpoints
{
    public static IEndpointRouteBuilder MapJobEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("jobs", Create)
            .WithOpenApi()
            .WithDisplayName("Create")
            .WithDescription("Create a job")
            .WithTags("Jobs");

        builder.MapGet("jobs/{id}", GetById)
            .WithOpenApi()
            .WithDisplayName("GetById")
            .WithDescription("Get a job by id")
            .WithTags("Jobs");

        return builder;
    }


    private static async Task<IResult> GetById([FromRoute] JobId id, [FromServices] IFacade<JobsModule> executor,
        CancellationToken cancellationToken)
    {
        var result = await executor.ExecuteAsync(new GetJob(id), cancellationToken);
        return result.ToHttpResult();
    }

    private static async Task<IResult> Create([FromBody] CreateJobModel model,
        [FromServices] IFacade<JobsModule> executor,
        CancellationToken cancellationToken)
    {
        var createResult = await executor.ExecuteAsync(new CreateJob(JobId.New(), model.UserId), cancellationToken);
        return createResult.ToHttpResult();
    }
}