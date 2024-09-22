using AutoMapper;
using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.DataAccess.Exceptions;
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
            throw new InventoryNotFoundException(productId, warehouseId);
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
            throw new EntityNotFoundException("There is nothing in warehouses");
        }
        return inventories.Select(i => _mapper.Map<InventoryDto>(i));
    }
    public async Task AddStockAsync(InventoryDto dto)
    {
        IsInvalidInput(dto.ProductId, dto.WarehouseId, dto.Quantity);

        var inventory = await _inventoryRepository.GetAsync(dto.ProductId, dto.WarehouseId);
        if (inventory == null)
        {
            // Create a new inventory if it doesn't exist
            inventory = new Inventory
            {
                ProductId = dto.ProductId,
                WarehouseId = dto.WarehouseId,
                Quantity = 0 // Start with 0 and add the quantity
            };
            await _inventoryRepository.AddAsync(inventory);
        }
        using var transaction = _inventoryRepository.BeginTransaction();
        try
        {
            inventory.Quantity += dto.Quantity;
            await _inventoryRepository.UpdateAsync(inventory);

            await _inventoryRepository.СommitTransactionAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            await _inventoryRepository.RollBackTransactionAsync();

            _logger.LogWarning($"Concurrency conflict when updating stock for product {dto.ProductId} in warehouse {dto.WarehouseId}");
            throw;
        }
        _logger.LogInformation($"Stock added for product {dto.ProductId} " +
                $"in warehouse {dto.WarehouseId}. New quantity: {inventory.Quantity}");
    }
    public async Task RemoveStockAsync(InventoryDto dto)
    {
        IsInvalidInput(dto.ProductId, dto.WarehouseId, dto.Quantity);

        var inventory = await GetStockAsync(dto.ProductId, dto.WarehouseId);

        if (inventory.Quantity < dto.Quantity)
        {
            throw new InvalidOperationException("Cannot remove more stock than is available.");
        }

        inventory.Quantity -= dto.Quantity;
        await UpdateStockAsync(inventory);

        // Log the stock change
    }
    public async Task UpdateStockAsync(InventoryDto dto)
    {
        try
        {
            var inventoryDto = await GetStockAsync(dto.ProductId, dto.WarehouseId);

            inventoryDto.Quantity = dto.Quantity;
            await _inventoryRepository.UpdateAsync(_mapper.Map<Inventory>(inventoryDto));
        }
        catch (DbUpdateConcurrencyException)
        {
            _logger.LogWarning($"Concurrency conflict when updating stock for product {dto.ProductId} in warehouse {dto.WarehouseId}");
            throw;
        }
        _logger.LogInformation($"Stock updated for product {dto.ProductId} in warehouse {dto.WarehouseId}. New quantity: {dto.Quantity}");
    }
    public async Task<IEnumerable<InventoryDto>> GetStockByProductAsync(int productId)
    {
        IsInvalidInput(productId);
        var inventories = await _inventoryRepository.GetByProductAsync(productId);
        if (inventories == null || !inventories.Any())
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
    public async Task<bool> ExistStockForProductAsync(int productId)
    {
        var stocks = await GetStockByProductAsync(productId);

        return stocks.Any(p => p.Quantity > 0);
    }
    public async Task TransferStockAsync(TransferStockDto dto)
    {
        var sourceInventory = await GetStockAsync(dto.ProductId, dto.SourceWarehouseId);
        var targetInventory = await GetStockAsync(dto.ProductId, dto.TargetWarehouseId);

        if (sourceInventory.Quantity < dto.Quantity)
        {
            throw new InvalidOperationException("Insufficient stock in warehouse");
        }

        sourceInventory.Quantity -= dto.Quantity;
        targetInventory.Quantity += dto.Quantity;

        await UpdateStockAsync(sourceInventory);
        await UpdateStockAsync(targetInventory);
    }
    private void IsInvalidInput(int productId = 1, int warehouseId = 1, int quantity = 1)
    {
        if (productId <= 0) throw new ArgumentException("Invalid product ID", nameof(productId));
        if (warehouseId <= 0) throw new ArgumentException("Invalid warehouse ID", nameof(warehouseId));
        if (quantity < 0) throw new ArgumentException("Quantity cannot be negative", nameof(quantity));
    }
}
