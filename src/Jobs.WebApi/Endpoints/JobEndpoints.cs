using Foundation.Core.CQRS.Design;
using Jobs.Application.Commands;
using Jobs.Application.Queries;
using Jobs.Domain.Entities.Identifiers;
using Jobs.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.WebApi.Endpoints;

public static class JobEndpoints
{
    public static IEndpointRouteBuilder MapJobEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("jobs/create-and-execute", CreateAndExecute)
            .WithOpenApi()
            .WithDisplayName("CreateAndExecute")
            .WithDescription("Create an execute a job")
            .WithTags("Jobs");

        builder.MapGet("jobs/{id}", GetById)
            .WithOpenApi()
            .WithDisplayName("GetById")
            .WithDescription("Get a job by id")
            .WithTags("Jobs");

        return builder;
    }


    private static async Task<IResult> GetById([FromRoute] JobId id, [FromServices] IQueryExecutor executor,
        CancellationToken cancellationToken)
    {
        var result = await executor.ExecuteAsync(new GetJob(id), cancellationToken);
        return result.ToHttpResult();
    }

    private static async Task<IResult> CreateAndExecute([FromBody] CreateJobModel model,
        [FromServices] ICommandExecutor executor,
        CancellationToken cancellationToken)
    {
        var result = await executor.ExecuteAsync(new CreateAndExecuteJob(JobId.New(), model.UserId), cancellationToken);
        return result.ToHttpResult();
    }
}