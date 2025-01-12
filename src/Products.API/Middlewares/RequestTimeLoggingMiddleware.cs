using System.Diagnostics;

namespace Products.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopWatch.Stop();

        // Check if the request took longer than 4000 milliseconds (4 seconds)
        if (stopWatch.Elapsed.TotalMilliseconds > 4000)
        {
            logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                context.Request.Method,
                context.Request.Path,
                stopWatch.Elapsed.TotalMilliseconds);
        }
    }
}
