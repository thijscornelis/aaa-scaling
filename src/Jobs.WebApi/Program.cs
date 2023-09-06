using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Foundation.Core.DependencyInjection;
using Foundation.Core.TypedIdentifiers;
using Jobs.Application.Commands;
using Jobs.Domain.Abstractions;
using Jobs.Domain.Entities.Identifiers;
using Jobs.Infrastructure.EntityFramework;
using Jobs.Infrastructure.EntityFramework.Repositories;
using Jobs.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(x =>
    {
        x.OrderActionsBy(a => a.HttpMethod);
        x.CustomSchemaIds(type => type.ToString().Replace('+', '-'));

        x.MapType<JobId>(() => new OpenApiSchema()
        {
            Type = nameof(Guid),
            Example = new OpenApiString(Guid.Parse("3F840752-E927-4F27-AE40-FE4A269E677D").ToString())
        });
        x.MapType<UserId>(() => new OpenApiSchema()
        {
            Type = nameof(Guid),
            Example = new OpenApiString(Guid.Parse("CD8E1D39-A1C2-41A9-AAEC-39959AE08BA7").ToString())
        });
    })
    .WithCommandAndQueryResponsibilitySegregation(typeof(CreateJob).Assembly)
    .AddTransient<IJobRepository, JobRepository>()
    .AddTransient<IUserRepository, UserRepository>()
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
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapJobEndpoints();
app.MapUserEndpoints();

app.Run();

void ConfigureJsonSerializerOptions(JsonSerializerOptions jsonSerializerOptions)
{
    jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    jsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
    jsonSerializerOptions.WriteIndented = true;
    jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    var identifierAssembly = typeof(JobId).Assembly;
    var converters = identifierAssembly.GetTypes().Where(x => x.IsAssignableTo(typeof(JsonConverter))).ToList();
    foreach (var converter in converters)
    {
        var instance = Activator.CreateInstance(converter);
        if (instance is JsonConverter jsonConverter)
        {
            jsonSerializerOptions.Converters.Add(jsonConverter);
        }
    }
}