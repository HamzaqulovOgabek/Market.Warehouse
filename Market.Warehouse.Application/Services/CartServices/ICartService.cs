namespace E_CommerceProjectDemo.Application.Services.CartServices
{
    public interface ICartService
    {
        Task AddToCartAsync(CartDto dto);
        Task ClearCartAsync(int userId);
        Task<CartDetailsDto> GetCartByUserIdAsync(int userId);
        Task RemoveFromCartAsync(int userId, int productId);
        Task<int> RemoveFromCartAsync(CartDto dto);
    }
}