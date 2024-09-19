using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq.Expressions;

namespace Market.Warehouse.DataAccess.Repository.InventoryRepository;

public class InventoryRepository : IInventoryRepository, IDisposable
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;
    public InventoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Inventory> GetAsync(int productId, int warehouseId)
    {
        return await _context.Inventories
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(i => 
                                    i.ProductId == productId && 
                                    i.WarehouseId == warehouseId);
    }
    public IQueryable<Inventory> GetAllStock()
    {
        return _context.Inventories;
    }
    public async Task<IEnumerable<Inventory>> GetByProductAsync(int productId)
    {
        return await _context.Inventories
            .Where(i => i.ProductId == productId)
            .ToListAsync();
    }
    public async Task<IEnumerable<Inventory>> GetByWarehouseAsync(int warehouseId)
    {
        return await _context.Inventories
            .Where(i => i.WarehouseId == warehouseId)
            .ToListAsync();
    }
    public async Task AddAsync(Inventory inventory)
    {
        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Inventory inventory)
    {
        try
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }
    public async Task DeleteAsync(int productId, int warehouseId)
    {
        var inventory = await GetAsync(productId, warehouseId);
        if (inventory != null)
        {
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
        }
    }
    public IDbContextTransaction BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
        return _transaction;
    }
    public async Task СommitTransactionAsync()
    {
         await _context.Database.CommitTransactionAsync();
    }
    public async Task RollBackTransactionAsync()
    {
         await _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
