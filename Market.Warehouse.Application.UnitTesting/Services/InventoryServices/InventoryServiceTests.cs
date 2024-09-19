using Moq;
using NUnit.Framework;
using Market.Warehouse.DataAccess.Repository.InventoryRepository;

namespace Market.Warehouse.Application.UnitTesting.Services.InventoryServices
{
    [TestFixture]
    public class InventoryServiceTests
    {
        public InventoryServiceTests()
        {
            
        }

        public void AddStockAsync_InventoryExist_ShouldIncreaseStock()
        {

            // Arrange
            var inventoryRepository = new Mock<IInventoryRepository>
            int productId = 1;
            int warehouseId = 1;
            int initialQuantity = 10;
            int addQuantity = 5;


        }


    }
}
