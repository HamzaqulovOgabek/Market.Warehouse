using Market.Warehouse.DataAccess.Context;
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
}
