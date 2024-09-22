using Market.Warehouse.Application.Dto;

namespace Market.Warehouse.Application.Services.InventoryServices
{
    public interface IInventoryService
    {
        Task<InventoryDto> GetStockAsync(int productId, int warehouseId);
        IQueryable<InventoryDto> GetAllStock(BaseSortFilterDto dto);
        Task AddStockAsync(InventoryDto dto);
        Task RemoveStockAsync(InventoryDto dto);
        Task UpdateStockAsync(InventoryDto dto);
        Task<IEnumerable<InventoryDto>> GetStockByProductAsync(int productId);
        Task<IEnumerable<InventoryDto>> GetStockByWarehouseAsync(int warehouseId);
        Task<bool> ExistStockForProductAsync(int productId);
        Task TransferStockAsync(TransferStockDto dto);
    }
}
