using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Filters;
using Products.Domain.Interfaces.Repositories;
using Products.Infrastructure.Persistence;

namespace Products.Infrastructure.Repositories;

internal sealed class ProductsRepository(StoreDbContext storeDbContext) : IProductsRepository
{
    private readonly StoreDbContext _storeDbContext = storeDbContext ?? throw new ArgumentNullException(nameof(storeDbContext));

    public async Task<List<Product>> GetProductsAsync(ProductFilter filter)
    {
        IQueryable<Product> query = _storeDbContext.Products.Include(p => p.Category);

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filter.MinPrice);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filter.MaxPrice);
        }

        if (filter.StartDate.HasValue && filter.EndDate.HasValue)
        {
            query = query.Where(p => p.CreatedDate >= filter.StartDate && p.CreatedDate <= filter.EndDate);
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == filter.CategoryId);
        }

        return await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsByPrice(decimal minPrice, decimal maxPrice, int pageNumber, int pageSize)
    {
        return await _storeDbContext.Products
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsByDate(DateTime startDate, DateTime endDate, int pageNumber, int pageSize)
    {
        return await _storeDbContext.Products
            .Where(p => p.CreatedDate >= startDate && p.CreatedDate <= endDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsByCategory(Guid categoryId, int pageNumber, int pageSize)
    {
        return await _storeDbContext.Products
            .Where(p => p.CategoryId == categoryId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllProducts(int pageNumber, int pageSize)
    {
        return await _storeDbContext.Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _storeDbContext.Products.FindAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        _storeDbContext.Products.Add(product);

        await _storeDbContext.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _storeDbContext.Products.Update(product);

        await _storeDbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        Product? product = await _storeDbContext.Products.FindAsync(id);

        if (product != null)
        {
            _storeDbContext.Products.Remove(product);

            await _storeDbContext.SaveChangesAsync();
        }
    }
}