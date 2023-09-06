using System.Text.Json;
using System.Text.Json.Serialization;
using Foundation.Core.DependencyInjection;
using Jobs.Application.Commands;
using Jobs.Domain.Abstractions;
using Jobs.Domain.Entities.Identifiers;
using Jobs.Infrastructure.EntityFramework;
using Jobs.Infrastructure.EntityFramework.Repositories;
using Jobs.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(x =>
    {
        x.OrderActionsBy(a => a.HttpMethod);
        x.CustomSchemaIds(type => type.ToString().Replace('+', '-'));
        x.MapTypedIdentifier<JobId>();
        x.MapTypedIdentifier<UserId>();
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