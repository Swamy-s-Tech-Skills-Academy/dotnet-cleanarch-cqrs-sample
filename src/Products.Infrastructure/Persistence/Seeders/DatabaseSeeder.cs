using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Products.Infrastructure.Persistence.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var provider = scope.ServiceProvider;
        var context = provider.GetRequiredService<StoreDbContext>();
        var categoriesSeeder = provider.GetRequiredService<ICategoriesSeeder>();
        var productsSeeder = provider.GetRequiredService<IProductsSeeder>();
        var logger = provider.GetRequiredService<ILogger<DatabaseSeederLogger>>();

        try
        {
            context.Database.Migrate();
            await categoriesSeeder.SeedAsync();
            await productsSeeder.SeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the Store DB.");
        }
    }
}