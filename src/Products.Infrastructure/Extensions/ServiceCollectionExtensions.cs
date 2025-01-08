using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Interfaces.Repositories;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Persistence.Seeders;
using Products.Infrastructure.Repositories;

namespace Products.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 2,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        }).EnableSensitiveDataLogging());

        services.AddScoped<ICategoriesSeeder, CategoriesSeeder>();

        services.AddScoped<IProductsSeeder, ProductsSeeder>();

        services.AddScoped<ICategoriesRepository, CategoriesRepository>();

        services.AddScoped<IProductsRepository, ProductsRepository>();
    }
}
