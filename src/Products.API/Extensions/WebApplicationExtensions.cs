using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Persistence.Seeders;

namespace Products.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseHttpPipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            using IServiceScope? scope = app.Services.CreateScope();
            IServiceProvider? services = scope.ServiceProvider;

            try
            {
                StoreDbContext? context = services.GetRequiredService<StoreDbContext>();
                context.Database.Migrate(); // Ensure database is created/updated

                ICategoriesSeeder? categoriesSeeder = services.GetRequiredService<ICategoriesSeeder>();
                await categoriesSeeder.SeedAsync();

                IProductsSeeder? productsSeeder = services.GetRequiredService<IProductsSeeder>();
                await productsSeeder.SeedAsync();
            }
            catch (Exception ex)
            {
                ILogger<Program>? logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the Store DB.");
            }
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}