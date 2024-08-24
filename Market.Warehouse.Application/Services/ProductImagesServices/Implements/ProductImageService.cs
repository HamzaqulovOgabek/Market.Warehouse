using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.ProductImagesServices;
using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.DataAccess.Repository.ProductImagesRepository;
using Market.Warehouse.Domain.Models;
using Market.Warehouse.ViewModel.ProductImage;
using Microsoft.Extensions.Configuration;

public class ProductImageService : IProductImageService
{
    private readonly string _storagePath;
    private readonly IProductImagesRepository _productImagesRepository;

    public ProductImageService(IConfiguration configuration, IProductImagesRepository productImagesRepository)
    {
        _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        _productImagesRepository = productImagesRepository;
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<int> CreateAsync(ProductImage productImage)
    {
        var id = await _productImagesRepository.CreateAsync(productImage);
        return id;
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> DownloadImageAsync(string id)
    {
        var filePath = Path.Combine(_storagePath, id);

        if (File.Exists(filePath))
        {
            return await File.ReadAllBytesAsync(filePath);
        }

        throw new FileNotFoundException("Image not found.", id);
    }

    public async Task<ProductImage> GetAsync(int id)
    {
        var image = await _productImagesRepository.GetByIdAsync(id);
        if(image == null)
        {
            throw new EntityNotFoundException("Image not found");
        }
        return image;

    }

    public IQueryable<ProductImageBaseDto> GetList(BaseSortFilterDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(ProductImagesUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
