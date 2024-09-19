namespace Market.Warehouse.Application.Services.InventoryServices;

public class InventoryNotFoundException : Exception
{
    public InventoryNotFoundException(int productId, int warehouseId)
        : base($"No inventory found for product {productId} in warehouse {warehouseId}")
    {

    }
}
