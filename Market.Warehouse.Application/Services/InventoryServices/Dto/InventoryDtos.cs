namespace Market.Warehouse.Application.Services.InventoryServices;

public class InventoryDto
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
}
public class TransferStockDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int SourceWarehouseId { get; set; }
    public int TargetWarehouseId { get; set; }
}