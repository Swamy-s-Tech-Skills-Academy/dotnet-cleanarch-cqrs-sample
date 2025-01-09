using Microsoft.EntityFrameworkCore;
using Products.Application.Extensions;
using Products.Infrastructure.Extensions;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Persistence.Seeders;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

WebApplication? app = builder.Build();

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

app.Run();
