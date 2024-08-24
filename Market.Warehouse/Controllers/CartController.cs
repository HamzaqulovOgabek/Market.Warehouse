using E_CommerceProjectDemo.Application.Services.CartServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_project_Demo.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCartAsync(int userId)
    {
        var cart = await _cartService.GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            return NotFound();
        }
        return Ok(cart);
    }

    [HttpPost("{userId}/add")]
    public async Task<IActionResult> AddToCartAsync(int userId, [FromBody] AddToCartRequest request)
    {
        await _cartService.AddToCartAsync(userId, request.ProductId, request.Quantity);
        return Ok();
    }

    [HttpDelete("{userId}/remove/{productId}")]
    public async Task<IActionResult> RemoveFromCartAsync(int userId, int productId)
    {
        await _cartService.RemoveFromCartAsync(userId, productId);
        return Ok();
    }

    [HttpDelete("{userId}/clear")]
    public async Task<IActionResult> ClearCartAsync(int userId)
    {
        await _cartService.ClearCartAsync(userId);
        return Ok();
    }
}
