using Market.Warehouse.Application.Services.InventoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.Warehouse.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;
    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpGet("{productId}/{warehouseId}")]
    public async Task<ActionResult<InventoryDto>> GetStock(int productId, int warehouseId)
    {
        var stock = await _service.GetStockAsync(productId, warehouseId);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAllStock()
    {
        var stocks = await _service.GetAllStockAsync();
        return Ok(stocks);
    }
    [HttpPost("add")]
    public async Task<IActionResult> AddStock(AddStockDto addStockDto)
    {
        await _service.AddStockAsync(
            addStockDto.ProductId,
            addStockDto.WarehouseId,
            addStockDto.Quantity);
        return Ok();
    }
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveStock(RemoveStockDto removeStockDto)
    {
        await _service.RemoveStockAsync(
            removeStockDto.ProductId,
            removeStockDto.WarehouseId,
            removeStockDto.Quantity);
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> UpdateStock(UpdateStockDto updateStockDto)
    {
        await _service.UpdateStockAsync(
            updateStockDto.ProductId,
            updateStockDto.WarehouseId,
            updateStockDto.NewQuantity);
        return Ok();
    }
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetStockByProduct(int productId)
    {
        var stock = await _service.GetStockByProductAsync(productId);

        return Ok(stock);
    }
    [HttpGet("warehouse/{warehouseId}")]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetStockByWarehouse(int warehouseId)
    {
        var stock = await _service.GetStockByWarehouseAsync(warehouseId);
        return Ok(stock);
    }
}