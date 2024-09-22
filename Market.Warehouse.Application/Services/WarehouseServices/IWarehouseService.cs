using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.InventoryServices;

namespace Market.Warehouse.Application.Services.WarehouseServices;

public interface IInventoryService
{
    Task<InventoryDto> GetStockAsync(int productId, int warehouseId);
    IQueryable<InventoryDto> GetAllStock(BaseSortFilterDto dto);
    Task AddStockAsync(int productId, int warehouseId, int quantity);
    Task RemoveStockAsync(int productId, int warehouseId, int quantity);
    Task UpdateStockAsync(int productId, int warehouseId, int newQuantity);
    Task<IEnumerable<InventoryDto>> GetStockByProductAsync(int productId);
    Task<IEnumerable<InventoryDto>> GetStockByWarehouseAsync(int warehouseId);
    Task<bool> ExistStockForProductAsync(int productId);
}