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
        var totalDistinctProducts = cartItemDtos
            .Select(ci => ci.ProductId)
            .Distinct()
            .Count();
        var totalProductCount = cartItemDtos.Sum(ci => ci.Quantity);
        var totalDiscount = cartItemDtos
            .Sum(ci => ci.DiscountPrice != 0 ? (ci.Price - ci.DiscountPrice) * ci.Quantity
                                             : 0);
        var totalPrice = cartItemDtos
            .Sum(ci => ci.DiscountPrice != 0 ? ci.DiscountPrice * ci.Quantity
                                             : ci.Price * ci.Quantity);

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
    public async Task AddToCartAsync(CartDto dto)
    {
        int productId = dto.ProductId;
        int userId = dto.UserId;
        int quantity = dto.Quantity;

        // Find the user's cart
        var existingCartItem = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.ProductId == productId && c.UserId == userId)
            .FirstOrDefaultAsync();

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
    public async Task<int> RemoveFromCartAsync(CartDto dto)
    {
        // Remove the specified quantity of the product from the cart
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(i => i.UserId == dto.UserId && i.ProductId == dto.ProductId);
        if(cartItem.Quantity < dto.Quantity)
            throw new InvalidOperationException("Not enough quantity in the cart");

        cartItem.Quantity -= dto.Quantity;
        await _context.SaveChangesAsync();
        return cartItem.Quantity;
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