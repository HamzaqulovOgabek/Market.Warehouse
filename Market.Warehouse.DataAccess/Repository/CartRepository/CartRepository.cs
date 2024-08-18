using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.DataAccess.Repository.CartRepository;

public class CartRepository : BaseRepository<CartItem, int>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    public Task GetByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<CartItem?> GetUserCartAsync(int userId)
    {
        return await Context.Carts
            //.Include(c => c.CartItems)
            //.ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    
    
}
