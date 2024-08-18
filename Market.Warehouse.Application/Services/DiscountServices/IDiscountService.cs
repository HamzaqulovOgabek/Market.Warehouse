using Market.Warehouse.Application.Dto;

namespace Market.Warehouse.Application.Services.DiscountServices;

public interface IDiscountService
{
    Task<int> Create(DiscountBaseDto discount);
    Task Delete(int id);
    Task<DiscountDto> Get(int id);
    IQueryable<DiscountUpdateDto> GetList(BaseSortFilterDto dto);
    Task<int> Update(DiscountUpdateDto discount);
}
