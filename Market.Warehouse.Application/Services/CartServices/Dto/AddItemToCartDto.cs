namespace Market.Warehouse.Application.Services.CartServices;

public class AddItemToCartDto
{
    public required int UserId { get; set; }
    public required int ProductId { get; set; }
    public required int Quantity { get; set; }
}
