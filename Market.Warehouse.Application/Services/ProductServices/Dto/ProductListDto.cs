using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductListDto : ProductUpdateDto
{
    public decimal DiscountPrice { get; set; }
    public string? Discount { get; set; }
    public int RewierCount { get; set; }
};
