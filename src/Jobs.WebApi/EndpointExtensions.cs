using Foundation.Core.Abstractions;

namespace Jobs.WebApi
{
    public static class EndpointExtensions
    {
        public static IResult ToHttpResult(this ExecutionResult result)
        {
            if (result.HasFailed)
            {
                return Results.Json(result, statusCode: 500);
            }
            return Results.Json(result, statusCode: 200);
        }
    }
}
