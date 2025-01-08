using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence.Seeders;

public class ProductsSeeder(StoreDbContext storeDbContext) : IProductsSeeder
{
    private readonly StoreDbContext _storeDbContext = storeDbContext ?? throw new ArgumentNullException(nameof(storeDbContext));

    public async Task SeedAsync()
    {
        if (!_storeDbContext.Products.Any()) // Check if data exists
        {
            var electronicsCategory = _storeDbContext.Categories.FirstOrDefault(c => c.Name == "Electronics");
            if (electronicsCategory != null)
            {
                var products = new List<Product>
                    {
                        new() { Id = Guid.NewGuid(), Name = "Laptop", Price = 1200.00m, CategoryId = electronicsCategory.Id, CreatedDate = DateTime.Now },
                        new() { Id = Guid.NewGuid(), Name = "Smartphone", Price = 800.00m, CategoryId = electronicsCategory.Id, CreatedDate = DateTime.Now },
                        // Add more products
                    };

                await _storeDbContext.Products.AddRangeAsync(products);
                await _storeDbContext.SaveChangesAsync();
            }
        }
    }
}