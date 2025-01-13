using Products.Shared.DTOs;

namespace Products.Web.Services;

public class CategoryService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ProductsAPI");

    public async Task<List<CategoryDto>?> GetCategoriesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/categories");
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"Error fetching categories: {ex.Message}");
            return null; // Or re-throw if you prefer handling the exception in the component
        }
    }
}