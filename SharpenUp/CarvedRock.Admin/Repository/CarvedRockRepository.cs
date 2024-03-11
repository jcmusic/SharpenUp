using CarvedRock.Admin.Data;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Admin.Repository
{
    public class CarvedRockRepository : ICarvedRockRepository
    {
        private readonly ProductContext _context;

        public CarvedRockRepository(ProductContext productContext)
        {
            _context = productContext;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }


        public async Task<Product?> GetProductAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }


        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product; // will have updated ID value
        }


        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Products.Any(e => e.Id == product.Id))
                {
                    // product exists and update exception is real
                    throw;
                }
                // caught and swallowed exception can occur if the other update was a delete
            }
        }


        public async Task DeleteProductAsync(int id)
        {
            //if (id == 3)
            //{
            //    throw new Exception("Simulated exception trying to remove product!");
            //}

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categorys.FirstOrDefaultAsync(m => m.Id == categoryId);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categorys.ToListAsync();
        }
    }
}
