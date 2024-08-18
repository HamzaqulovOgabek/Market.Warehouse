using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository.CartRepository;

public interface ICartRepository : IBaseRepository<CartItem, int>
{
    Task GetByUserIdAsync(int userId);
    Task<CartItem?> GetUserCartAsync(int userId);
}
