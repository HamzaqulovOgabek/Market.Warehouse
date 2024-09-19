using AutoMapper;
using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.DataAccess.Repository.InventoryRepository;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Market.Warehouse.Application.Services.InventoryServices;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ILogger<InventoryService> _logger;
    private readonly IMapper _mapper;

    public InventoryService(
        IInventoryRepository inventoryRepository,
        ILogger<InventoryService> logger,
        IMapper mapper)
    {
        _inventoryRepository = inventoryRepository;
        _logger = logger;
        this._mapper = mapper;
    }

    public async Task<InventoryDto> GetStockAsync(int productId, int warehouseId)
    {
        IsInvalidInput(productId, warehouseId);

        var inventory = await _inventoryRepository.GetAsync(productId, warehouseId);
        if (inventory == null)
        {
            throw new InvalidOperationException($"No inventory found for product {productId} in warehouse {warehouseId}.");
        }

        return _mapper.Map<InventoryDto>(inventory);
    }
    public IQueryable<InventoryDto> GetAllStock(BaseSortFilterDto dto)
    {
        var inventories = _inventoryRepository
            .GetAllStock()
            .SortFilter(dto);

        if (!inventories.Any())
        {
            throw new Exception("There is nothing in warehouses");
        }
        return inventories.Select(i => _mapper.Map<InventoryDto>(i));
    }
    public async Task AddStockAsync(int productId, int warehouseId, int quantity)
    {
        IsInvalidInput(productId, warehouseId, quantity);

        var inventory = await _inventoryRepository.GetAsync(productId, warehouseId);
        if (inventory == null)
        {
            // Create a new inventory if it doesn't exist
            inventory = new Inventory
            {
                ProductId = productId,
                WarehouseId = warehouseId,
                Quantity = 0 // Start with 0 and add the quantity
            };
            await _inventoryRepository.AddAsync(inventory);
        }
        using var transaction = _inventoryRepository.BeginTransaction();
        try
        {
            inventory.Quantity += quantity;
            await _inventoryRepository.UpdateAsync(inventory);

            await _inventoryRepository.СommitTransactionAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            await _inventoryRepository.RollBackTransactionAsync();
            // Log the stock change (you can add actual logging here)
            _logger.LogWarning($"Concurrency conflict when updating stock for product {productId} in warehouse {warehouseId}");
            throw;
        }
        _logger.LogInformation($"Stock added for product {productId} " +
                $"in warehouse {warehouseId}. New quantity: {inventory.Quantity}");
    }
    public async Task RemoveStockAsync(int productId, int warehouseId, int quantity)
    {
        IsInvalidInput(productId, warehouseId, quantity);

        var inventory = await _inventoryRepository.GetAsync(productId, warehouseId);
        if (inventory == null)
        {
            throw new InvalidOperationException($"Not found inventory for product {productId} in warehouse {warehouseId}.");
        }

        if (inventory.Quantity < quantity)
        {
            throw new InvalidOperationException("Cannot remove more stock than is available.");
        }

        inventory.Quantity -= quantity;
        await _inventoryRepository.UpdateAsync(inventory);

        // Log the stock change
    }
    public async Task UpdateStockAsync(int productId, int warehouseId, int newQuantity)
    {
        try
        {
            var inventory = await _inventoryRepository.GetAsync(productId, warehouseId);
            if (inventory == null)
            {
                throw new InventoryNotFoundException(productId, warehouseId);
            }

            inventory.Quantity = newQuantity;
            await _inventoryRepository.UpdateAsync(inventory);
        }
        catch (DbUpdateConcurrencyException)
        {
            _logger.LogWarning($"Concurrency conflict when updating stock for product {productId} in warehouse {warehouseId}");
            throw;
        }
        _logger.LogInformation($"Stock updated for product {productId} in warehouse {warehouseId}. New quantity: {newQuantity}");
    }
    public async Task<IEnumerable<InventoryDto>> GetStockByProductAsync(int productId)
    {
        IsInvalidInput(productId);
        var inventories = await _inventoryRepository.GetByProductAsync(productId);
        if (inventories == null || inventories.Any())
        {
            throw new Exception("Inventory not found");
        }
        return inventories.Select(_mapper.Map<InventoryDto>);
    }
    public async Task<IEnumerable<InventoryDto>> GetStockByWarehouseAsync(int warehouseId)
    {
        IsInvalidInput(warehouseId: warehouseId);
        var inventories = await _inventoryRepository.GetByWarehouseAsync(warehouseId);

        if (inventories == null || !inventories.Any())
        {
            throw new Exception("Inventory not found");
        }
        return inventories.Select(_mapper.Map<InventoryDto>);
    }

    private void IsInvalidInput(int productId = 1, int warehouseId = 1, int quantity = 1)
    {
        if (productId <= 0) throw new ArgumentException("Invalid product ID", nameof(productId));
        if (warehouseId <= 0) throw new ArgumentException("Invalid warehouse ID", nameof(warehouseId));
        if (quantity < 0) throw new ArgumentException("Quantity cannot be negative", nameof(quantity));
    }
}
