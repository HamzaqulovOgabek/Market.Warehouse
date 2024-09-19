namespace Market.Warehouse.Application.Services.InventoryServices;

public class InventoryDto
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
}
public class AddStockDto
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
}

public class RemoveStockDto
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateStockDto
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int NewQuantity { get; set; }
}
