using System.Reflection;
using Module.Jobs.Application;
using Module.Jobs.Application.Abstractions;
using Module.Jobs.Application.Commands;
using Module.Jobs.Domain.Abstractions;
using Module.Jobs.Infrastructure.EntityFramework;
using Module.Jobs.Infrastructure.EntityFramework.Repositories;
using Shared.Core.DependencyInjection;
using Shared.Core.EntityFramework;
using Shared.Core.MediatR;

namespace Scaling.SimpleWebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.WithMediatR();

        var modules = new List<IModule>()
        {
            new JobModule()
        };

        foreach (var module in modules)
        {
            module.ConfigureServices(builder.Services);
        }

        var app = builder.Build();

        foreach (var module in modules)
        {
            module.Configure(app);
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");



        app.Run();
    }

    private static void AddJobModule(IServiceCollection services)
    {

        services.AddTransient<IJobRepository, JobRepository>();
        services.WithEntityFramework<JobDbContext>("Jobs");
    }
}