using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.DataAccess.Repository.CategoryRepository;

public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
    public async Task CategorizeProductAsync(int productId, int categoryId)
    {
        var product = await Context.Products.FindAsync(productId);
        if (product != null)
        {
            product.CategoryId = categoryId;
            await Context.SaveChangesAsync();
        }
    }

    public override Task<Category?> GetByIdAsync(int id)
    {
        var category = Context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }
}

