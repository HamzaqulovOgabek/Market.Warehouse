using Market.Warehouse.Application.Dto;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.ReviewServices;

public interface IReviewService
{
    Task<int> CreateAsync(ReviewDtoBase dto);
    Task DeleteAsync(int id);
    Task<Review> GetAsync(int id);
    IQueryable<Review> GetList(BaseSortFilterDto dto);
    Task<int> UpdateAsync(ReviewUpdateDto dto);
}
