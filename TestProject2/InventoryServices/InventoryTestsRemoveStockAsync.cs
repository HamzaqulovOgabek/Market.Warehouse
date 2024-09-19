using Market.Warehouse.Application.Services.InventoryServices;
using Market.Warehouse.DataAccess.Repository.InventoryRepository;
using Market.Warehouse.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace Market.Warehouse.Application.UnitTesting.InventoryServices;

internal class InventoryTestsRemoveStockAsync
{
    internal int _productId;
    internal int _warehouseId;
    internal int _initialQuantity;
    internal int _removeQuantity;
    internal int _negativeQuantity;
    internal int _zeroQuantity;
    internal Mock<IInventoryRepository> _inventoryRepository;
    private Mock<ILogger<InventoryService>> _logger;
    internal InventoryService _inventoryService;
    [SetUp]
    public void SetUp()
    {
        _inventoryRepository = new Mock<IInventoryRepository>();
        _logger = new Mock<ILogger<InventoryService>>();
        _inventoryService = new InventoryService(_inventoryRepository.Object, _logger.Object, null);

        _productId = 1;
        _warehouseId = 1;
        _initialQuantity = 10;
        _removeQuantity = 5;
        _negativeQuantity = -4;
        _zeroQuantity = 0;

    }
    [Test]
    public async Task RemoveStockAsync_StockExist_CanbeRemovedAsync()
    {
        var existingInventory = new Inventory
        {
            ProductId = 1,
            Quantity = _initialQuantity,
            WarehouseId = _warehouseId
        };
        _inventoryRepository
            .Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(Task.FromResult(existingInventory));

        await _inventoryService.RemoveStockAsync(_productId, _warehouseId, _removeQuantity);

        _inventoryRepository.Verify(repo => repo.UpdateAsync(
            It.Is<Inventory>(i => i.Quantity == _initialQuantity - _removeQuantity)));
    }
    [Test]
    public async Task RemoveStockAsync_StockExistButInsufficientQuantityAsync()
    {
        var existingInventory = new Inventory
        {
            ProductId = 1,
            Quantity = _initialQuantity,
            WarehouseId = _warehouseId
        };
        _inventoryRepository
            .Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(Task.FromResult(existingInventory));

        await _inventoryService.RemoveStockAsync(_productId, _warehouseId, _zeroQuantity);

        _inventoryRepository.Verify(repo => repo.UpdateAsync(
            It.Is<Inventory>(i => i.Quantity == _initialQuantity - _zeroQuantity)));
    }
    [Test]
    public void RemoveStockAsync_NegativeQuantity_ThrowsArgumentException()
    {
        //Act and Arrange
        Assert.That(
            () => _inventoryService.RemoveStockAsync(_productId, _warehouseId, _negativeQuantity),
            Throws.ArgumentException);
    }
    [Test]
    public void RemoveStockAsync_WhenInventoryDoesNotExist_ThrowsInvalidOperationException()
    {
        _inventoryRepository.Setup(repo => repo.GetAsync(_productId, _warehouseId))
            .ReturnsAsync(() => null);
        var exception = Assert.ThrowsAsync<InvalidOperationException>(
            () => _inventoryService.RemoveStockAsync(_productId, _warehouseId, _initialQuantity));

        Assert.That(exception.Message, Does.Contain("not found").IgnoreCase);
    }
    [Test]
    public async Task RemoveStockAsync_ConcurrentAccess_ShouldHandleConcurrencyCorrectly()
    {
        var existingInventory = new Inventory
        {
            ProductId = 1,
            Quantity = _initialQuantity,
            WarehouseId = _warehouseId
        };
        _inventoryRepository
            .Setup(repo => repo.GetAsync(_productId, _warehouseId))
            .Returns(Task.FromResult(existingInventory));

        var tasks = new List<Task>
        {
            Task.Run(() => _inventoryService.RemoveStockAsync(_productId, _warehouseId, _removeQuantity)),
            Task.Run(() => _inventoryService.RemoveStockAsync(_productId, _warehouseId, _removeQuantity)),
        };
        //Act
        await Task.WhenAll(tasks);

        //Assert 
        _inventoryRepository.Verify(repo =>
            repo.GetAsync(_productId, _warehouseId), Times.Exactly(2));
         _inventoryRepository.Verify(repo =>
            repo.UpdateAsync(It.IsAny<Inventory>()), Times.Once);

    }
}
