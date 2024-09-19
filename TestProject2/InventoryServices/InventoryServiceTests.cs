using Castle.Core.Logging;
using Market.Warehouse.Application.Services.InventoryServices;
using Market.Warehouse.DataAccess.Repository.InventoryRepository;
using Market.Warehouse.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace Market.Warehouse.Application.UnitTesting.Services.InventoryServices;

[TestFixture]
public class InventoryServiceTestsAddStockAsync
{
    internal int _productId;
    internal int _warehouseId;
    internal int _initialQuantity;
    internal int _addQuantity;
    internal int _negativeQuantity;
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
        _addQuantity = 5;
        _negativeQuantity = -4;

    }
    [Test]
    public async Task AddStockAsync_InventoryExist_ShouldIncreaseStock()
    {
        // Arrange
        var existingInventory = new Inventory
        {
            ProductId = 1,
            Quantity = _initialQuantity,
            WarehouseId = _warehouseId
        };
        _inventoryRepository
            .Setup(ir => ir.GetAsync(1, 1))
            .Returns(Task.FromResult(existingInventory));

        //Act
        await _inventoryService.AddStockAsync(_productId, _warehouseId, _addQuantity);

        //Assert
        _inventoryRepository.Verify(repo => repo.UpdateAsync(It.Is<Inventory>(
            i => i.Quantity == _initialQuantity + _addQuantity)), Times.Once);
        _inventoryRepository.Verify(repo => repo.AddAsync(It.IsAny<Inventory>()), Times.Never);


    }
    [Test]
    public async Task AddStockAsync_InventoryDoesNotExist_AddInventory()
    {
        _inventoryRepository
            .Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((Inventory)null);
        await _inventoryService.AddStockAsync(_productId, _warehouseId, _initialQuantity);

        _inventoryRepository.Verify(repo => repo.AddAsync(
            It.Is<Inventory>(i => i.Quantity == _initialQuantity)), Times.Once);
        _inventoryRepository.Verify(repo => repo.UpdateAsync(
            It.IsAny<Inventory>()), Times.Once);
    }
    [Test]
    public void AddStockAsync_WhenNegativeQuantity_ThrowsInvalidOperationExceptionAsync()
    {
        //Act and Assert
        var exception = Assert.ThrowsAsync<ArgumentException>(
            () => _inventoryService.AddStockAsync(_productId, _warehouseId, _negativeQuantity));
        _inventoryRepository.Verify(repo => repo.UpdateAsync(
            It.IsAny<Inventory>()), Times.Never);
    }
}
