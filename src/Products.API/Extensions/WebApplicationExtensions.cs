using Products.API.Middlewares;

namespace Products.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseHttpPipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseMiddleware<RequestTimeLoggingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}