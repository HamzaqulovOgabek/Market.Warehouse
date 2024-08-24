using Market.Warehouse.Domain.Enums;
using Market.Warehouse.Domain.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;
[Table(nameof(Product))]
public class Product : Auditable<int>, IHaveState
{
    [RegularExpression("^[\\w0-9.,_]+$", ErrorMessage = "Use only words or numbers")]
    public required string Name { get; set; }
    public string? Description { get; set; }
    [ProductPriceValidation]
    public decimal Price { get; set; }
    public string? Color { get; set; }
    public string? Features { get; set; }
    public string? Material { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
    public int Quantity { get; set; }
    public int BrandId { get; set; }
    public int? DiscountId { get; set; }
    public int? CategoryId { get; set; }
    public int WareHouseId { get; set; }    
    public State State { get; set; } = State.ACTIVE;

    public Brand Brand { get; set; } = null!;
    public Discount? Discount { get; set; }
    public Category? Category { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<ProductImage>? ProductImages { get; set; }
}
