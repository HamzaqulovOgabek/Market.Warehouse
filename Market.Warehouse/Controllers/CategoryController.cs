using E_CommerceProjectDemo.Application.Services.CategoryServices;
using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.CategoryServices;
using Market.Warehouse.Application.Services.CategoryServices.Dto;
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
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await _service.GetAsync(id));
    }
    [HttpPost]
    public IActionResult GetListAsync([FromBody] BaseSortFilterDto options)
    {
        return Ok(_service.GetList(options));
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CategoryUpdateDto dto)
    {
        return Ok(await _service.UpdateAsync(dto));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
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
