using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(ProductImage))]
public class ProductImage : BaseEntity<int>
{
    public required string ImageUrl { get; set; }
    public required int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
