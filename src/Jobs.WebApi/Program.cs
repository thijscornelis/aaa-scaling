using Foundation.Core.DependencyInjection;
using Jobs.Application.Commands;
using Jobs.Application.Queries;
using Jobs.Domain.Entities.Identifiers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var modules = new IModule[]
{
    new Jobs.Application.Module(),
    new Jobs.Domain.Module(),
    new Jobs.Infrastructure.Module(),
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(type => type.ToString().Replace('+', '-')));
builder.Services.RegisterModules(modules);

var app = builder.Build();
app.ConfigureModule(modules);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("jobs", ([FromServices] IMediator mediator, CancellationToken cancellationToken) => mediator.Send(new CreateJob(JobId.New()), cancellationToken)).WithOpenApi();
app.MapGet("jobs", ([FromServices] IMediator mediator, CancellationToken cancellationToken) => mediator.Send(new GetJobs(), cancellationToken)).WithOpenApi();
app.MapGet("jobs/{id}", ([FromRoute]JobId id, [FromServices] IMediator mediator, CancellationToken cancellationToken) => mediator.Send(new GetJob(id), cancellationToken)).WithOpenApi();
app.MapDelete("jobs/{id}", ([FromRoute] JobId id, [FromServices] IMediator mediator, CancellationToken cancellationToken) => mediator.Send(new DeleteJob(id), cancellationToken)).WithOpenApi();
app.Run();
