using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository.ProductImagesRepository;

public class ProductImagesRepository : BaseRepository<ProductImage, int>, IProductImagesRepository
{
    public ProductImagesRepository(AppDbContext context) : base(context)
    {

    }
}
