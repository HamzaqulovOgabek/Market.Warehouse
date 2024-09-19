namespace Market.Warehouse.Application.Services.ProductServices;
public class ProductBaseDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Color { get; set; }
    public string? Features { get; set; }
    public string? Material { get; set; }
    public int BrandId { get; set; }
    public int? DiscountId { get; set; }
    public int? CategoryId { get; set; }
}
