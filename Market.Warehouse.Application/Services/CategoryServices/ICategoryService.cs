using Market.Warehouse.Application.Dto;

namespace Market.Warehouse.Application.Services.CategoryServices;

public interface ICategoryService
{
    Task<CategoryDto> GetAsync(int id);
    IQueryable<CategoryListDto> GetList(BaseSortFilterDto dto);
    Task<int> CreateAsync(CategoryDtoBase dto);
    Task<int> UpdateAsync(CategoryUpdateDto dto);
    Task Delete(int id);
    Task CategorizeProductAsync(int productId, int categoryId);
}
