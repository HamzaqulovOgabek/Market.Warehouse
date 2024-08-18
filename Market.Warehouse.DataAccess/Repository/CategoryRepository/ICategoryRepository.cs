using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository.CategoryRepository;

public interface ICategoryRepository : IBaseRepository<Category, int>
{
    Task CategorizeProductAsync(int productId, int categoryId);
}
