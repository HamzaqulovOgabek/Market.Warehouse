using Market.Warehouse.Application.Dto;
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
    public async Task<ActionResult<InventoryDto>> GetStock([FromHeader] int productId, int warehouseId)
    {
        var stock = await _service.GetStockAsync(productId, warehouseId);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock);
    }

    [HttpGet]
    public ActionResult<IEnumerable<InventoryDto>> GetAllStock([FromBody] BaseSortFilterDto  dto)
    {
        var stocks = _service.GetAllStock(dto);
        return Ok(stocks);
    }
    [HttpPost]
    public async Task<IActionResult> AddStock([FromBody] InventoryDto dto)
    {
        await _service.AddStockAsync(dto);
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> RemoveStock([FromBody]InventoryDto dto)
    {
        await _service.RemoveStockAsync(dto);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateStock([FromBody] InventoryDto dto)
    {
        await _service.UpdateStockAsync(dto);
            
        return Ok();
    }
    [HttpGet("{productId}")]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetStockByProduct([FromQuery] int productId)
    {
        var stock = await _service.GetStockByProductAsync(productId);

        return Ok(stock);
    }
    [HttpGet("{warehouseId}")]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetStockByWarehouse([FromQuery] int warehouseId)
    {
        var stock = await _service.GetStockByWarehouseAsync(warehouseId);
        return Ok(stock);
    }
    [HttpGet("{productId}")]
    public async Task<IActionResult> ExistStockForProductAsync([FromQuery] int productId)
    {
        return Ok(await _service.ExistStockForProductAsync(productId));
    }
}