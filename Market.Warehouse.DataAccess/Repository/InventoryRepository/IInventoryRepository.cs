using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Market.Warehouse.DataAccess.Repository.InventoryRepository
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetAsync(int productId, int warehouseId);
        IQueryable<Inventory> GetAllStock();
        Task<IEnumerable<Inventory>> GetByProductAsync(int productId);
        Task<IEnumerable<Inventory>> GetByWarehouseAsync(int warehouseId);
        Task AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task DeleteAsync(int productId, int warehouseId);
        IDbContextTransaction BeginTransaction();
        Task СommitTransactionAsync();
        Task RollBackTransactionAsync();
    }
}
