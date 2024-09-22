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
    public async Task<IActionResult> GetAsync(int id)
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
    public async Task<IActionResult> CreateAsync([FromBody] ProductBaseDto dto)
    {
        return Ok(await _service.Create(dto));
    }
    [HttpPost]
    public async Task<IActionResult> AddListOfProducts([FromBody] List<ProductBaseDto> dtos)
    {
        return Ok(await _service.AddAListOfProducts(dtos));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] ProductDto dto)
    {
        return Ok(await _service.Update(dto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.Delete(id);

        return Ok();
    }
}
