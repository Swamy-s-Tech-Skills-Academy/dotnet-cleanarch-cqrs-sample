using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Repositories;

public interface ICategoriesRepository
{
    Task<List<Category>> GetAllCategoriesAsync();

    Task<Category?> GetCategoryByIdAsync(Guid id);

    Task AddCategoryAsync(Category category);

    Task UpdateCategoryAsync(Category category);

    Task DeleteCategoryAsync(Guid id);
}