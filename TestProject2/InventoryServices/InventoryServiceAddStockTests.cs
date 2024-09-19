using Market.Warehouse.Application.Services.InventoryServices;
using Market.Warehouse.Application.UnitTesting.InventoryServices;
using Market.Warehouse.DataAccess.Repository.InventoryRepository;
using Market.Warehouse.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace Market.Warehouse.Application.UnitTesting.Services.InventoryServices;

[TestFixture]
public class InventoryServiceAddStockTests  : InventoryServiceTestBase
{
    [Test]
    public async Task AddStockAsync_InventoryExist_ShouldIncreaseStock()
    {
        // Arrange
        SetupExistingInventory();

        //Act
        await InventoryService.AddStockAsync(ProductId, WarehouseId, AddQuantity);

        //Assert
        InventoryRepository.Verify(repo => repo.UpdateAsync(It.Is<Inventory>(
            i => i.Quantity == InitialQuantity + AddQuantity)), Times.Once);
        InventoryRepository.Verify(repo => repo.AddAsync(It.IsAny<Inventory>()), Times.Never);
    }
    [Test]
    public async Task AddStockAsync_InventoryDoesNotExist_AddInventory()
    {
        InventoryRepository
            .Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((Inventory)null);
        await InventoryService.AddStockAsync(ProductId, WarehouseId, InitialQuantity);

        InventoryRepository.Verify(repo => repo.AddAsync(
            It.Is<Inventory>(i => i.Quantity == InitialQuantity)), Times.Once);
        InventoryRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Inventory>()), Times.Once);
    }
    [Test]
    public void AddStockAsync_WhenNegativeQuantity_ThrowsArgumentException()
    {
        //Act and Assert
        var action = () => InventoryService.AddStockAsync(ProductId, WarehouseId, NegativeQuantity);

        Assert.That(action, Throws.ArgumentException);
        InventoryRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Inventory>()), Times.Never);
    }
}
