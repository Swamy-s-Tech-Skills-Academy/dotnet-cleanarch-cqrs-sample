using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence.Seeders;

public class CategoriesSeeder(StoreDbContext storeDbContext) : ICategoriesSeeder
{
    private readonly StoreDbContext _storeDbContext = storeDbContext ?? throw new ArgumentNullException(nameof(storeDbContext));

    public async Task SeedAsync()
    {
        if (!_storeDbContext.Categories.Any()) // Check if data exists
        {
            var categories = new List<Category>
                {
                    new() { Id = Guid.NewGuid(), Name = "Electronics" },
                    new() { Id = Guid.NewGuid(), Name = "Books" },
                    new() { Id = Guid.NewGuid(), Name = "Clothing" },
                    // Add more categories
                };

            await _storeDbContext.Categories.AddRangeAsync(categories);
            await _storeDbContext.SaveChangesAsync();
        }
    }
}