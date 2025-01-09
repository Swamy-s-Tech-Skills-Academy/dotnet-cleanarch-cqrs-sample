using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence.Seeders;

public class ProductsSeeder(StoreDbContext storeDbContext) : IProductsSeeder
{
    private readonly StoreDbContext _storeDbContext = storeDbContext ?? throw new ArgumentNullException(nameof(storeDbContext));

    public async Task SeedAsync()
    {
        if (!_storeDbContext.Products.Any()) // Check if data exists
        {
            foreach (Category? category in _storeDbContext.Categories)
            {
                List<Product> products = [];
                Random random = new();

                for (int i = 0; i < 10; i++)
                {
                    // Generate random date within the last year
                    DateTime randomDate = DateTime.Now.AddDays(-random.Next(365));

                    products.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Product {category.Name} {i + 1}",
                        Price = random.Next(10, 1000) * 0.99m, // Generate random price between $10 and $999.99
                        CategoryId = category.Id,
                        CreatedDate = randomDate
                    });
                }

                await _storeDbContext.Products.AddRangeAsync(products);
            }

            await _storeDbContext.SaveChangesAsync();
        }
    }
}