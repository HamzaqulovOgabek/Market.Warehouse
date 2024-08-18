using Market.Warehouse.Application.Dto;
using Market.Warehouse.ViewModel.ProductImage;

namespace Market.Warehouse.Application.Services.ProductImagesServices;

public interface IProductImagesService
{
    Task<int> Create(ProductImageBaseDto dto);
    Task Delete(int id);
    Task<ProductImageBaseDto> Get(int id);
    IQueryable<ProductImageBaseDto> GetList(BaseSortFilterDto dto);
    Task<int> Update(ProductImagesUpdateDto dto);
}
