using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(CartItem))]
public class CartItem : BaseEntity<int>, IHaveState 
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
    public Product? Product { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.Now;
    public State State { get; set; } = State.ACTIVE;

}
