using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Infrastructure.Repositories;

public class CategoryRepository(StoreDbContext storeDbContext) : ICategoryRepository
{
    private readonly StoreDbContext _storeDbContext = storeDbContext;

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _storeDbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _storeDbContext.Categories.FindAsync(id);
    }
    public async Task AddCategoryAsync(Category category)
    {
        _storeDbContext.Categories.Add(category);
        await _storeDbContext.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _storeDbContext.Categories.Update(category);
        await _storeDbContext.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _storeDbContext.Categories.FindAsync(id);
        if (category != null)
        {
            _storeDbContext.Categories.Remove(category);
            await _storeDbContext.SaveChangesAsync();
        }
    }
}