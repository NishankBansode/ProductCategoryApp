using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategoryApp.Models;

namespace ProductCategoryApp.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetPaginatedCategoriesAsync(int page, int pageSize);
        Task<int> GetTotalCategoriesCountAsync();
        Task AddCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        SelectList GetCategorySelectList(int? selectedCategoryId = null);
    }
}
