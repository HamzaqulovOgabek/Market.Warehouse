using Market.Warehouse.Application.Dto;
using Market.Warehouse.Domain.Models;
using Market.Warehouse.ViewModel.ProductImage;

namespace Market.Warehouse.Application.Services.ProductImagesServices;

public interface IProductImageService
{
    Task<int> CreateAsync(ProductImage productImage);
    Task Delete(int id);
    Task<byte[]> DownloadImageAsync(string fileName);
    Task<ProductImage> GetAsync(int id);
    IQueryable<ProductImageBaseDto> GetList(BaseSortFilterDto dto);
    Task<int> Update(ProductImagesUpdateDto dto);
}
