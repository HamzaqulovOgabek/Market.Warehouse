using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository.ProductRepository;

public interface IProductRepository : IBaseRepository<Product, int>
{
    Task AddAListOfProducts(List<Product> products);
    Task<bool> ExistsByNameAsync(string name);
}
