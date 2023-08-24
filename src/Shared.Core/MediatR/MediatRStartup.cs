using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core.MediatR;

public static class MediatRStartup
{
    public static IServiceCollection WithMediatR(this IServiceCollection services) =>
        services.AddMediatR(x =>
        {
            x.AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(StopwatchPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehaviour<,>));
            x.AddOpenBehavior(typeof(UnitOfWorkPipelineBehaviour<,>));
        });
}