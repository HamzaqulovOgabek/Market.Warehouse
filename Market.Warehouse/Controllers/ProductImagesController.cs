//using Market.Warehouse.Application.Dto;
//using Market.Warehouse.Application.Services.ProductImagesServices;
//using Market.Warehouse.ViewModel.ProductImage;
//using Microsoft.AspNetCore.Mvc;

//namespace Market.Warehouse.Controllers;

//[ApiController]
//[Route("[controller]/[action]")]
//public class ProductImagesController : ControllerBase
//{
//    private readonly IProductImagesService service;

//    public ProductImagesController(IProductImagesService service)
//    {
//        this.service = service;
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> Get(int id)
//    {
//        var images = await service.Get(id);
//        return Ok(images);
//    }
//    [HttpPost]
//    public IActionResult GetList(BaseSortFilterDto options)
//    {
//        var images = service.GetList(options);
//        return Ok(images);
//    }
//    [HttpPost]
//    public async Task<IActionResult> Create(ProductImageBaseDto dto)
//    {
//        var imageId = await service.Create(dto);
//        return Ok(imageId);
//    }
//    [HttpPut]
//    public async Task<IActionResult> Update(ProductImagesUpdateDto dto)
//    {
//        var imageId = await service.Update(dto);
//        return Ok(imageId);
//    }
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        await service.Delete(id);
//        return Ok();
//    }

//}
