using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository.ProductRepository;

public interface IProductRepository : IBaseRepository<Product, int>
{
    Task<bool> ExistsByNameAsync(string name);
}
