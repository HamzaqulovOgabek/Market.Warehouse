namespace E_CommerceProjectDemo.Application.Services.CartServices
{
    public interface ICartService
    {
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task ClearCartAsync(int userId);
        Task<CartDetailsDto> GetCartByUserIdAsync(int userId);
        Task RemoveFromCartAsync(int userId, int productId);
    }
}