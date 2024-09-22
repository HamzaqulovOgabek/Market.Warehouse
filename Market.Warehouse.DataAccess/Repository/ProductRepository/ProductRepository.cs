using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Enums;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.DataAccess.Repository.ProductRepository;

public class ProductRepository : BaseRepository<Product, int>, IProductRepository
{
    private readonly AppDbContext context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
    public async Task<bool> ExistsByNameAsync(string name)
    {
        var isExistName = await Context.Products.AnyAsync(p => p.Name == name);
        return isExistName;
    }
    public override Task<Product?> GetByIdAsync(int id)
    {
        var product = Context.Products
            .Include(p => p.Brand)
            .Include(p => p.Discount)
            .Include(p => p.Category)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id && p.State == State.ACTIVE);

        return product;
    }
    public async Task AddAListOfProducts(List<Product> products)
    {
        await Context.Products.AddRangeAsync(products);
    }
}
