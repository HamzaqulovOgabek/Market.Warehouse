using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.DiscountServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.Warehouse.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _service;

    public DiscountController(IDiscountService service)
    {
        _service = service;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var discount = await _service.Get(id);
        return Ok(discount);
    }
    [HttpPost]
    public IActionResult GetList([FromBody] BaseSortFilterDto dto)
    {
        var discounts = _service.GetList(dto);
        return Ok(discounts);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] DiscountBaseDto discount)
    {
        var discounts = await _service.Create(discount);
        return Ok(discounts);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] DiscountUpdateDto dto)
    {
        var discounts = await _service.Update(dto);
        return Ok(discounts);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
}
