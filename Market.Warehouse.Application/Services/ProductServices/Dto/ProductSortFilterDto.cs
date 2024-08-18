using Market.Warehouse.Application.Dto;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductSortFilterDto : BaseSortFilterDto
{
    public int? BrandId { get; set; }
    public double? DiscountId { get; set; }
    public int? CategoryId { get; set; }
    public decimal? FromPrice { get; set; }
    public decimal? ToPrice { get; set; }
}
