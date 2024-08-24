using Market.Warehouse.Application.Dto;

namespace Market.Warehouse.Application.Services.BrandServices;

public interface IBrandService
{
    Task<int> CreateAsync(BrandDtoBase dto);
    Task DeleteAsync(int id);
    IQueryable<BrandDto> GetAll(BaseSortFilterDto dto);
    Task<BrandDto> GetByIdAsync(int id);
    Task<int> UpdateAsync(BrandDto dto);
}
