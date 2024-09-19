using Market.Warehouse.Application.Services.InventoryServices;
using Market.Warehouse.Application.UnitTesting.Services.InventoryServices;
using Market.Warehouse.DataAccess.Repository.InventoryRepository;
using Market.Warehouse.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace Market.Warehouse.Application.UnitTesting.InventoryServices;
[TestFixture]
public class InventoryTestsRemoveStockAsync : InventoryServiceAddStockTests
{
    [Test]
    public async Task RemoveStockAsync_StockExist_CanbeRemovedAsync()
    {
        SetupExistingInventory();

        await InventoryService.RemoveStockAsync(ProductId, WarehouseId, RemoveQuantity);

        InventoryRepository.Verify(repo => repo.UpdateAsync(
            It.Is<Inventory>(i => i.Quantity == InitialQuantity - RemoveQuantity)));
    }
    [Test]
    public async Task RemoveStockAsync_StockExistButInsufficientQuantityAsync()
    {
        SetupExistingInventory();

        await InventoryService.RemoveStockAsync(ProductId, WarehouseId, ZeroQuantity);

        InventoryRepository.Verify(repo => repo.UpdateAsync(
            It.Is<Inventory>(i => i.Quantity == InitialQuantity - ZeroQuantity)));
    }
    [Test]
    public void RemoveStockAsync_NegativeQuantity_ThrowsArgumentException()
    {
        //Act and Arrange
        Assert.That(
            () => InventoryService.RemoveStockAsync(ProductId, WarehouseId, NegativeQuantity),
            Throws.ArgumentException);
    }
    [Test]
    public void RemoveStockAsync_WhenInventoryDoesNotExist_ThrowsInvalidOperationException()
    {
        InventoryRepository
            .Setup(repo => repo.GetAsync(ProductId, WarehouseId))
            .ReturnsAsync(() => null);
        var exception = Assert.ThrowsAsync<InvalidOperationException>(
            () => InventoryService.RemoveStockAsync(ProductId, WarehouseId, InitialQuantity));

        Assert.That(exception.Message, Does.Contain("not found").IgnoreCase);
    }
    [Test]
    public async Task RemoveStockAsync_ConcurrentAccess_ShouldHandleConcurrencyCorrectly()
    {
        SetupExistingInventory();

        var tasks = new List<Task>
        {
            Task.Run(() => InventoryService.RemoveStockAsync(ProductId, WarehouseId, RemoveQuantity)),
            Task.Run(() => InventoryService.RemoveStockAsync(ProductId, WarehouseId, RemoveQuantity)),
        };
        //Act
        await Task.WhenAll(tasks);

        //Assert 
        InventoryRepository.Verify(repo =>
            repo.GetAsync(ProductId, WarehouseId), Times.Exactly(2));
         InventoryRepository.Verify(repo =>
            repo.UpdateAsync(It.IsAny<Inventory>()), Times.Once);
    }
}
