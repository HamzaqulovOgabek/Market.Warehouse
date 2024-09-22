using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.CategoryServices;

namespace Market.Warehouse.Application.Services.ProductServices
{
    public interface IProductService
    {
        Task<int> Create(ProductBaseDto dto);
        Task<ProductDto> Get(int id);
        IQueryable<ProductListDto> GetList(ProductSortFilterDto options);
        Task<int> Update(ProductDto dto);
        Task Delete(int id);
        Task<int> AddAListOfProducts(List<ProductBaseDto> dtos);
    }
}
