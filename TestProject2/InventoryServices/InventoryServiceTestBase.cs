using Market.Warehouse.Application.Services.InventoryServices;
using Market.Warehouse.DataAccess.Repository.InventoryRepository;
using Market.Warehouse.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace Market.Warehouse.Application.UnitTesting.InventoryServices;

public class InventoryServiceTestBase
{
    protected const int ProductId = 1;
    protected const int WarehouseId = 1;
    protected const int InitialQuantity = 10;
    protected const int AddQuantity = 5;
    protected const int RemoveQuantity = 5;
    protected const int NegativeQuantity = -4;
    protected const int ZeroQuantity = 0;

    protected Mock<IInventoryRepository> InventoryRepository;
    protected Mock<ILogger<InventoryService>> Logger;
    protected InventoryService InventoryService;

    [SetUp]
    public virtual void SetUp()
    {
        InventoryRepository = new Mock<IInventoryRepository>();
        Logger = new Mock<ILogger<InventoryService>>();
        InventoryService = new InventoryService(InventoryRepository.Object, Logger.Object, null);
    }

    protected void SetupExistingInventory()
    {
        var existingInventory = new Inventory
        {
            ProductId = ProductId,
            Quantity = InitialQuantity,
            WarehouseId = WarehouseId
        };
        InventoryRepository
            .Setup(ir => ir.GetAsync(ProductId, WarehouseId))
            .ReturnsAsync(existingInventory);
    }
}
