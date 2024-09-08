using E_CommerceProjectDemo.Application.Services.CategoryServices;
using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Services.CategoryServices.Dto;

namespace Market.Warehouse.Application.Services.CategoryServices;

public interface ICategoryService
{
    Task<CategoryCreateDto> GetAsync(int id);
    IQueryable<CategoryListDto> GetList(BaseSortFilterDto dto);
    Task<int> CreateAsync(CategoryCreateDto dto);
    Task<int> UpdateAsync(CategoryUpdateDto dto);
    Task Delete(int id);
    Task CategorizeProductAsync(int productId, int categoryId);
}
