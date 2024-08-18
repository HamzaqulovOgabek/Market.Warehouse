using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.Warehouse.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetAsync(id));
    }
    [HttpPost]
    public IActionResult GetList([FromBody] BaseSortFilterDto options)
    {
        return Ok(_service.GetList(options));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryDtoBase dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CategoryUpdateDto dto)
    {
        return Ok(await _service.UpdateAsync(dto));
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> CategorizeProductAsync(int productId, int categoryId)
    {
        await _service.CategorizeProductAsync(productId, categoryId);
        return Ok();
    }
}
