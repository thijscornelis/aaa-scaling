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

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(x => x.CustomSchemaIds(type => type.ToString().Replace('+', '-')))
    .WithCommandAndQueryResponsibilitySegregation(typeof(CreateJob).Assembly)
    .AddTransient<IJobRepository, JobRepository>()
    .WithEntityFramework(x =>
    {
        x.AddDbContext<DbContext, JobsDbContext>((sp, o) =>
        {
            var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Jobs");
            o.UseSqlServer(connectionString);
        });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwagger()
        .UseSwaggerUI();

app.UseHttpsRedirection();



app.Run();