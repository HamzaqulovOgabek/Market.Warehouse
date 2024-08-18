using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.DataAccess.Repository.CartItemRepository;

public class CartItemRepository : BaseRepository<CartItem, int>, ICartItemRepository
{
    public CartItemRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<List<CartItem>> GetByCartIdAsync(int cartId)
    {
        return await Context.CartItems
            .Where(ci => ci.Id == cartId)
            .ToListAsync();
    }
}
