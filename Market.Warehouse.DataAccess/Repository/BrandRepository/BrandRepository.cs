using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository.BrandRepository;

public class BrandRepository : BaseRepository<Brand, int>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
        
    }
}
