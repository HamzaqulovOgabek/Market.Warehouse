using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.ReviewServices;
using Market.Warehouse.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Market.Warehouse.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService service;
    public ReviewController(IReviewService service)
    {
        this.service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var review = await service.GetAsync(id);
        return Ok(review);
    }
    [HttpPost]
    public IActionResult GetList(BaseSortFilterDto dto)
    {
        var review =  service.GetList(dto);
        return Ok(review);
    }
    [HttpPost]
    public async Task<IActionResult> Create(ReviewDtoBase dto)
    {
        var reviewId = await service.CreateAsync(dto);
        return Ok(reviewId);
    }
    [HttpPut]
    public async Task<IActionResult> Update(ReviewUpdateDto dto)
    {
        var reviewId = await service.UpdateAsync(dto);
        return Ok(reviewId);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}
