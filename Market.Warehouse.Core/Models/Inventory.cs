using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(Inventory))]
public class Inventory : Auditable<int>, IHaveState
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
    public int Quantity { get; set; }
    public State State { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
    [ForeignKey(nameof(WarehouseId))]
    public Warehouse Warehouse { get; set; }
}
