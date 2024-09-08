using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductListDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? ReviewCount { get; set; }
    public double? Rating { get; set; }
    public int CategoryId { get; set; }
};
