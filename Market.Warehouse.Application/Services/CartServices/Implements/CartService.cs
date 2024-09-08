using AutoMapper;
using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProjectDemo.Application.Services.CartServices;

public class CartService : ICartService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CartService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartDetailsDto> GetCartByUserIdAsync(int userId)
    {
        var cartItems = await _context.CartItems
            .Include(x => x.Product)
            .ThenInclude(p => p.Discount)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        var cartItemDtos = _mapper.Map<List<CartItemDto>>(cartItems);

        // Calculate aggregates
        var totalDistinctProducts = cartItemDtos.Select(ci => ci.ProductId).Distinct().Count();
        var totalProductCount = cartItemDtos.Sum(ci => ci.Quantity);
        var totalDiscount = cartItemDtos.Sum(ci => (ci.Price - ci.DiscountPrice) * ci.Quantity);
        var totalPrice = cartItemDtos.Sum(ci => (ci.DiscountPrice) * ci.Quantity);

        var cartDetailsDto = new CartDetailsDto
        {
            UserId = userId,
            CartItems = _mapper.Map<List<CartItemDto>>(cartItems),
            TotalDistinctProducts = totalDistinctProducts,
            TotalProductCount = totalProductCount,
            TotalDiscount = totalDiscount,
            TotalPrice = totalPrice
        };
        return cartDetailsDto;
    }
    public async Task AddToCartAsync(int userId, int productId, int quantity)
    {
        // Find the user's cart
        var existingCartItem = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.ProductId == productId && c.UserId == userId)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity += quantity;
        }
        else
        {
            // Create a new cart if the user does not have one
            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                DateAdded = DateTime.Now,

            };
            await _context.CartItems.AddAsync(cartItem);
        }

        await _context.SaveChangesAsync();
    }
    public async Task RemoveFromCartAsync(int userId, int productId)
    {
        // Find the cart item to be removed
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(i => i.UserId == userId);
        if (cartItem != null)
        {
            _context.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
    public async Task ClearCartAsync(int userId)
    {
        // Find all cart items for the user
        var cartItems = await _context.CartItems
            .Where(c => c.UserId == userId)
            .ToListAsync();

        if (cartItems != null)
        {
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}