using Products.Domain.Entities;
using Products.Domain.Filters;

namespace Products.Domain.Interfaces.Repositories;

public interface IProductsRepository
{
    Task<List<Product>> GetProductsAsync(ProductFilter filter);

    Task<List<Product>> GetProductsByPrice(decimal minPrice, decimal maxPrice, int pageNumber, int pageSize);

    Task<List<Product>> GetProductsByDate(DateTime startDate, DateTime endDate, int pageNumber, int pageSize);

    Task<List<Product>> GetProductsByCategory(Guid categoryId, int pageNumber, int pageSize);

    Task<List<Product>> GetAllProducts(int pageNumber, int pageSize);

    Task<Product?> GetProductByIdAsync(int id);

    Task AddProductAsync(Product product);

    Task UpdateProductAsync(Product product);

    Task DeleteProductAsync(int id);
}