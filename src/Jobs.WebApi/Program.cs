using Foundation.Core.CQRS.Design;
using Foundation.Core.DependencyInjection;
using Jobs.Application.Commands;
using Jobs.Application.Queries;
using Jobs.Domain.Abstractions;
using Jobs.Domain.Entities.Identifiers;
using Jobs.Infrastructure.EntityFramework;
using Jobs.Infrastructure.EntityFramework.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(type => type.ToString().Replace('+', '-')));
builder.Services.WithMediator(typeof(CreateJob).Assembly);
builder.Services.WithEntityFramework();

builder.Services.AddDbContext<DbContext, JobsDbContext>((sp, o) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Jobs");
    o.UseSqlServer(connectionString);
});
builder.Services.AddTransient<IJobRepository, JobRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("jobs",
    ([FromServices] ICommandExecutor executor, CancellationToken cancellationToken) =>
        executor.ExecuteAsync(new CreateJob(JobId.New()), cancellationToken)).WithOpenApi();
app.MapGet("jobs",
    ([FromServices] IQueryExecutor executor, CancellationToken cancellationToken) =>
        executor.ExecuteAsync(new GetJobs(), cancellationToken)).WithOpenApi();
app.MapGet("jobs/{id}",
    ([FromRoute] JobId id, [FromServices] IQueryExecutor executor, CancellationToken cancellationToken) =>
        executor.ExecuteAsync(new GetJob(id), cancellationToken)).WithOpenApi();
app.MapDelete("jobs/{id}",
    ([FromRoute] JobId id, [FromServices] ICommandExecutor executor, CancellationToken cancellationToken) =>
        executor.ExecuteAsync(new DeleteJob(id), cancellationToken)).WithOpenApi();
app.Run();