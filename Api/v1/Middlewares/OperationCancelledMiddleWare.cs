namespace Api.v1.Middlewares;

public class OperationCancelledMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public OperationCancelledMiddleWare(RequestDelegate next, ILogger<OperationCancelledMiddleWare> logger)
        => (_next, _logger) = (next, logger);

    public async Task InvokeAsync(HttpContext context)
    {        
        try
        {
            await _next(context);
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation("Request was cancelled by user");
        }
    }
}

public static class RequestOperationCancelledMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestOperationCancelled(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OperationCancelledMiddleWare>();
    }
}
