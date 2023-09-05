using Foundation.Core.CQRS.Design;
using Jobs.Application.Commands;
using Jobs.Application.Queries;
using Jobs.Domain.Entities.Identifiers;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.WebApi;

public record CreateAndExecuteResult(CreateJob.Result? CreateResult, ExecuteJob.Result? ExecuteResult = null);

public static class JobEndpoints
{
    public static IEndpointRouteBuilder MapJobEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("jobs", Create)
            .WithOpenApi()
            .WithName("Create a job")
            .WithGroupName("Jobs");

        builder.MapPost("jobs/{id}/execute", Execute)
            .WithOpenApi()
            .WithName("Execute a job by id")
            .WithGroupName("Jobs");

        builder.MapPost("jobs/create-and-execute", CreateAndExecute)
            .WithOpenApi()
            .WithName("Create an execute a job")
            .WithGroupName("Jobs");

        builder.MapGet("jobs", GetAll)
            .WithOpenApi()
            .WithName("Get all jobs")
            .WithGroupName("Jobs");

        builder.MapGet("jobs/{id}", GetById)
            .WithOpenApi()
            .WithName("Get a job by id")
            .WithGroupName("Jobs");

        builder.MapDelete("jobs/{id}", DeleteById)
            .WithOpenApi()
            .WithName("Delete a job by id")
            .WithGroupName("Jobs");

        return builder;
    }

    private static async Task<DeleteJob.Result> DeleteById([FromRoute] JobId id, [FromServices] ICommandExecutor executor, CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new DeleteJob(id), cancellationToken);
    }

    private static async Task<GetJob.Result> GetById([FromRoute] JobId id, [FromServices] IQueryExecutor executor, CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new GetJob(id), cancellationToken);
    }

    private static async Task<GetJobs.Result> GetAll([FromServices] IQueryExecutor executor, CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new GetJobs(), cancellationToken);
    }

    private static async Task<CreateJob.Result> Create([FromServices] ICommandExecutor executor, CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new CreateJob(JobId.New()), cancellationToken);
    }

    private static async Task<ExecuteJob.Result> Execute([FromRoute] JobId id, [FromServices] ICommandExecutor executor, CancellationToken cancellationToken)
    {
        return await executor.ExecuteAsync(new ExecuteJob(id), cancellationToken);
    }
    private static async Task<CreateAndExecuteResult> CreateAndExecute([FromServices] ICommandExecutor executor, CancellationToken cancellationToken)
    {
        var createResult = await executor.ExecuteAsync(new CreateJob(JobId.New()), cancellationToken);
        if (createResult.HasFailed) return new CreateAndExecuteResult(createResult);

        var executeResult = await executor.ExecuteAsync(new ExecuteJob(JobId.New()), cancellationToken);
        return new CreateAndExecuteResult(createResult, executeResult);
    }
}