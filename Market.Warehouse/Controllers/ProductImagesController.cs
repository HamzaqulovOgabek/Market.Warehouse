using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.ProductImagesServices;
using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;
using Market.Warehouse.ViewModel.ProductImage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Market.Warehouse.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductImagesController : ControllerBase
{
    private readonly IProductImageService _service;
    private readonly string _storagePath;
    private readonly AppDbContext _dbContext;
    public ProductImagesController(IProductImageService productImageService, IConfiguration configuration, AppDbContext dbContext)
    {
        _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
        _service = productImageService;
        _dbContext = dbContext;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var images = await _service.GetAsync(id);
        return Ok(images);
    }
    [HttpPost]
    public IActionResult GetList(BaseSortFilterDto options)
    {
        var images = _service.GetList(options);
        return Ok(images);
    }
    [HttpPost("upload")]
    public async Task<int> UploadImageAsync(int productId, IFormFile file)
    {
        if (file.Length > 0 || file is not null)
        {
            var filePath = Path.Combine(_storagePath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var productImage = new ProductImage
            {
                ImageUrl = $"https://{file.FileName}",
                ProductId = productId,
                FileName = file.FileName,
                FilePath = filePath,
                ContentType = file.ContentType
            };

            var id = await _service.CreateAsync(productImage);
            return id;
        }

        throw new InvalidOperationException("File upload failed.");
    }
    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadAsync(int id)
    {
        try
        {
            var productImage = await _service.GetAsync(id);
            if (productImage == null)
            {
                return NotFound("Image not found.");
            }
            // Read the file bytes from the file system
            var fileBytes = await System.IO.File.ReadAllBytesAsync(productImage.FilePath);

            return File(fileBytes, productImage.ContentType, productImage.FileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }   
        
    }
