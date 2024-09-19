using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

public class StockMovement : Auditable<int>
{
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public MovementType Type { get; set; }
    public int WarehouseId { get; set; }
    [ForeignKey(nameof(WarehouseId))]
    public Warehouse Warehouse { get; set; }
}
