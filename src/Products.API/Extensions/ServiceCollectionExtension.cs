using Products.API.Middlewares;
using Products.Application.Extensions;
using Products.Infrastructure.Extensions;

namespace Products.API.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        services.AddApplication();

        services.AddInfrastructure(configuration);

        services.AddScoped<ErrorHandlingMiddleware>();

        services.AddScoped<RequestTimeLoggingMiddleware>();
    }
}