using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence.Seeders;

public class CategoriesSeeder(StoreDbContext storeDbContext) : ICategoriesSeeder
{
    private readonly StoreDbContext _storeDbContext = storeDbContext ?? throw new ArgumentNullException(nameof(storeDbContext));

    public async Task SeedAsync()
    {
        if (!_storeDbContext.Categories.Any()) // Check if data exists
        {
            List<Category> categories =
            [
                new() { Id = Guid.NewGuid(), Name = "Electronics", Description = "Televisions, computers, smartphones, and other electronic devices." },
                new() { Id = Guid.NewGuid(), Name = "Books", Description = "Fiction, non-fiction, textbooks, and other literary works." },
                new() { Id = Guid.NewGuid(), Name = "Clothing", Description = "Apparel for men, women, and children." },
                new() { Id = Guid.NewGuid(), Name = "Home & Kitchen", Description = "Furniture, cookware, appliances, and home decor." },
                new() { Id = Guid.NewGuid(), Name = "Toys & Games", Description = "Toys, board games, video games, and outdoor play equipment." },
                new() { Id = Guid.NewGuid(), Name = "Beauty & Personal Care", Description = "Cosmetics, skincare, haircare, and personal hygiene products." },
                new() { Id = Guid.NewGuid(), Name = "Sports & Outdoors", Description = "Sports equipment, outdoor gear, and fitness accessories." },
                new() { Id = Guid.NewGuid(), Name = "Automotive", Description = "Car parts, accessories, and maintenance supplies." },
                new() { Id = Guid.NewGuid(), Name = "Health & Household", Description = "Over-the-counter medications, cleaning supplies, and household essentials." },
                new() { Id = Guid.NewGuid(), Name = "Pet Supplies", Description = "Food, toys, accessories, and grooming supplies for pets." }
            ];

            await _storeDbContext.Categories.AddRangeAsync(categories);

            await _storeDbContext.SaveChangesAsync();
        }
    }
}