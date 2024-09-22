
public class InventoryServiceAddStockAsyncTests
{
    private readonly Mock<IInventoryRepository> _inventoryRepositoryMock;
    private readonly Mock<ILogger<InventoryService>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly InventoryService _inventoryService;

    public InventoryServiceAddStockAsyncTests()
    {
        _inventoryRepositoryMock = new Mock<IInventoryRepository>();
        _loggerMock = new Mock<ILogger<InventoryService>>();
        _mapperMock = new Mock<IMapper>();
        _inventoryService = new InventoryService(_inventoryRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task ShouldThrowExceptionWhenAddingStockWithInvalidProductId()
    {
        // Arrange
        int invalidProductId = 0;
        int warehouseId = 1;
        int quantity = 1;

        // Act
        await Assert.ThrowsAsync<ArgumentException>(() => _inventoryService.AddStockAsync(invalidProductId, warehouseId, quantity));

        // Assert
        _inventoryRepositoryMock.Verify(r => r.GetAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        _inventoryRepositoryMock.VerifyNoOtherCalls();
    }
}