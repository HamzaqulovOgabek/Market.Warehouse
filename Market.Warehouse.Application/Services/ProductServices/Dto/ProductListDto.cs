using System.Text.Json.Serialization;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductListDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? ReviewCount { get; set; }
    public double? Rating { get; set; }
    [JsonIgnore]
    public int CategoryId { get; set; }
    [JsonIgnore]
    public int DiscountId { get; set; }
    [JsonIgnore]
    public int BrandId { get; set; }
    [JsonIgnore]
    public int WarehouseId { get; set; }


};
