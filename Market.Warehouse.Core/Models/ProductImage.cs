using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(ProductImage))]
public class ProductImage : BaseEntity<int>, IHaveState
{
    public  string ImageUrl { get; set; }
    public required int ProductId { get; set; }
    public string FileName { get; set; }  // Name of the file
    public string FilePath { get; set; }  // Path where the file is stored
    public string ContentType { get; set; }  // MIME type of the file
    public State State { get; set; } = State.ACTIVE;

    public Product Product { get; set; } = null!;
}
