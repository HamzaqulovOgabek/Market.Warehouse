using E_CommerceProjectDemo.Application.Services.ProductServices;
using Market.Warehouse.Application.Services.DiscountServices;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductDto : ProductBaseDto
{
    public int Id { get; set; }
    public string? BrandName { get; set; }
    public decimal DiscountPrice { get; set; }
    public int ReviewCount { get; set; }
    public string? CategoryName { get; set; }
    public double? Rating { get; set; }
    public DiscountDto DiscountDto { get; set; }
    public List<ReviewDto> ReviewDtos { get; set; }
}
