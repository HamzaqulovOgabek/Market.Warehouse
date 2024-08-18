using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(CartItem))]
public class CartItem : BaseEntity<int>
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
    public int Quantity { get; set; }
    public Product? Product { get; set; }
}
