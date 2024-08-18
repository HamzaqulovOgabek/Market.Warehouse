using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductDto : ProductUpdateDto
{
    public string? BrandName { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountPrice { get; set; }
    public int ReviewCount { get; set; }
    public string? CategoryName { get; set; }

    public static explicit operator ProductDto(Product product)
    {
        decimal discountRate = product.Discount?.InterestRate ?? 0;
        decimal discountPrice = product.Price - product.Price * discountRate / 100;
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            BrandName = product.Brand?.Name,
            Price = product.Price,
            Color = product.Color,
            Features = product.Features,
            Quantity = product.Quantity,
            CategoryName = product.Category?.Name,
            Material = product.Material,
            ReviewCount = product.Reviews?.Count ?? 0,
            CategoryId = product.CategoryId,
            BrandId = product.BrandId,
            DiscountId = product.DiscountId,
            DiscountRate = discountRate,
            DiscountPrice = discountPrice
        };
    }
}
