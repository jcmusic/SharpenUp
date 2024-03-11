using CarvedRock.Admin.Data;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Admin.Repository
{
    public interface ICarvedRockRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<Category?> GetCategoryByIdAsync(int categoryId);
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
