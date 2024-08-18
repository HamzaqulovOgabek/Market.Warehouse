using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.Warehouse.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.Get(id));
    }
    [HttpPost]
    public IActionResult GetList([FromBody] ProductSortFilterDto options)
    {
        var product = _service.GetList(options);
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductBaseDto dto)
    {
        return Ok(await _service.Create(dto));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductDto dto)
    {
        return Ok(await _service.Update(dto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);

        return Ok();
    }
}
