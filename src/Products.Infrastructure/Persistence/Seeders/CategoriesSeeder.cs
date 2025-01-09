using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence.Seeders;

public class CategoriesSeeder(StoreDbContext storeDbContext) : ICategoriesSeeder
{
    private readonly StoreDbContext _storeDbContext = storeDbContext ?? throw new ArgumentNullException(nameof(storeDbContext));

    public async Task SeedAsync()
    {
        if (!_storeDbContext.Categories.Any()) // Check if data exists
        {
            List<Category>? categories =
                [
                    new() { Id = Guid.NewGuid(), Name = "Electronics" },
                    new() { Id = Guid.NewGuid(), Name = "Books" },
                    new() { Id = Guid.NewGuid(), Name = "Clothing" },
                    new() { Id = Guid.NewGuid(), Name = "Home & Kitchen" },
                    new() { Id = Guid.NewGuid(), Name = "Toys & Games" },
                    new() { Id = Guid.NewGuid(), Name = "Beauty & Personal Care" },
                    new() { Id = Guid.NewGuid(), Name = "Sports & Outdoors" },
                    new() { Id = Guid.NewGuid(), Name = "Automotive" },
                    new() { Id = Guid.NewGuid(), Name = "Health & Household" },
                    new() { Id = Guid.NewGuid(), Name = "Pet Supplies" }
                ];

            await _storeDbContext.Categories.AddRangeAsync(categories);

            await _storeDbContext.SaveChangesAsync();
        }
    }
}