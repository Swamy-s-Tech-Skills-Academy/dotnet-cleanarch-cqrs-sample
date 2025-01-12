using System.Net;
using System.Text.Json;

namespace Products.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    // Cache and reuse JsonSerializerOptions instance
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception Type: {ExceptionType}, Message: {Message}", ex.GetType().Name, ex.Message);
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message
        };

        var jsonResponse = JsonSerializer.Serialize(response, _jsonSerializerOptions);

        return context.Response.WriteAsync(jsonResponse);
    }
}