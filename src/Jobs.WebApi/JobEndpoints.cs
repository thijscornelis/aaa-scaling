﻿using Foundation.Core.CQRS.Design;
using Jobs.Application.Commands;
using Jobs.Application.Queries;
using Jobs.Domain.Entities.Identifiers;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.WebApi;

public record CreateAndExecuteResult(CreateJob.Result? CreateResult, ExecuteJob.Result? ExecuteResult = null);

public record CreateJobModel(UserId UserId);

public static class JobEndpoints
{
    public static IEndpointRouteBuilder MapJobEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("jobs", Create)
            .WithOpenApi()
            .WithDisplayName("Create")
            .WithDescription("Create a job")
            .WithTags("Jobs");

        builder.MapPost("jobs/{id}/execute", Execute)
            .WithOpenApi()
            .WithDisplayName("Execute")
            .WithDescription("Execute a job by id")
            .WithTags("Jobs");

        builder.MapPost("jobs/create-and-execute", CreateAndExecute)
            .WithOpenApi()
            .WithDisplayName("CreateAndExecute")
            .WithDescription("Create an execute a job")
            .WithTags("Jobs");

        builder.MapGet("jobs", GetAll)
            .WithOpenApi()
            .WithDisplayName("GetAll")
            .WithDescription("Get all jobs")
            .WithTags("Jobs");

        builder.MapGet("jobs/{id}", GetById)
            .WithOpenApi()
            .WithDisplayName("GetById")
            .WithDescription("Get a job by id")
            .WithTags("Jobs");

        builder.MapDelete("jobs/{id}", Delete)
            .WithOpenApi()
            .WithDisplayName("Delete")
            .WithDescription("Delete a job by id")
            .WithTags("Jobs");

        return builder;
    }

    private static Task<DeleteJob.Result> Delete([FromRoute] JobId id, [FromServices] ICommandExecutor executor,
        CancellationToken cancellationToken)
    {
        return executor.ExecuteAsync(new DeleteJob(id), cancellationToken);
    }

    private static async Task<GetJob.Result> GetById([FromRoute] JobId id, [FromServices] IQueryExecutor executor,
        CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new GetJob(id), cancellationToken);
    }

    private static async Task<GetJobs.Result> GetAll([FromServices] IQueryExecutor executor,
        CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new GetJobs(), cancellationToken);
    }

    private static async Task<CreateJob.Result> Create([FromBody] CreateJobModel model,
        [FromServices] ICommandExecutor executor,
        CancellationToken cancellationToken)
    {
        var result = await executor.ExecuteAsync(new CreateJob(JobId.New(), model.UserId), cancellationToken);
        return result;
    }

    private static async Task<ExecuteJob.Result> Execute([FromRoute] JobId id, [FromServices] ICommandExecutor executor,
        CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new ExecuteJob(id), cancellationToken);
    }

    private static async Task<CreateAndExecuteResult> CreateAndExecute([FromBody] CreateJobModel model,
        [FromServices] ICommandExecutor executor,
        CancellationToken cancellationToken)
    {
        var createResult = await executor.ExecuteAsync(new CreateJob(JobId.New(), model.UserId), cancellationToken);
        if (createResult.HasFailed) return new CreateAndExecuteResult(createResult);

        var executeResult = await executor.ExecuteAsync(new ExecuteJob(createResult.JobId), cancellationToken);
        return new CreateAndExecuteResult(createResult, executeResult);
    }
}