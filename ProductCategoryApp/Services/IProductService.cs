using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategoryApp.Models;

namespace ProductCategoryApp.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetPaginatedProductAsync(int page, int pageSize);
        Task<int> GetTotalProductCountAsync();
        Task AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Category>>GetCategoriesAsync();
    }
}
