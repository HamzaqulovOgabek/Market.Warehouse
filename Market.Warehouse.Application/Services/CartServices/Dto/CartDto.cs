using Market.Warehouse.Application.Services.CartServices;

public class CartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
}