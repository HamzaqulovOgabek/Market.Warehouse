using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.BrandServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.Warehouse.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;
        public BrandController(IBrandService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public IActionResult GetList([FromBody] BaseSortFilterDto dto)
        {
            return Ok(_service.GetAll(dto));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandDtoBase dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BrandDto dto)
        {
            return Ok(await _service.UpdateAsync(dto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

    }
}
