using Microsoft.EntityFrameworkCore;
using Products.Application.Extensions;
using Products.Infrastructure.Extensions;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<StoreDbContext>();
        context.Database.Migrate(); // Ensure database is created/updated

        var categoriesSeeder = services.GetRequiredService<ICategoriesSeeder>();
        await categoriesSeeder.SeedAsync();

        var productsSeeder = services.GetRequiredService<IProductsSeeder>();
        await productsSeeder.SeedAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
