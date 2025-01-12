using Products.API.Middlewares;
using Scalar.AspNetCore;

namespace Products.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseHttpPipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // https://localhost:7016/openapi/v1.json
            app.MapOpenApi();

            // https://localhost:7016/scalar/v1
            app.MapScalarApiReference();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseMiddleware<RequestTimeLoggingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}