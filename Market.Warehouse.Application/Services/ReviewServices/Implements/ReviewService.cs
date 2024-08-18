using Market.Warehouse.Application.Dto;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.DataAccess.Repository.ReviewRepository;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.ReviewServices;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;

    public ReviewService(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<Review> GetAsync(int id)
    {
        var review = await _repository.GetByIdAsync(id);
        if (review == null)
            throw new EntityNotFoundException("Entity not Found with this id");

        return review;
    }

    public IQueryable<Review> GetList(BaseSortFilterDto dto)
    {
        var review = _repository.GetAll();
        if (review == null)
            throw new EntityNotFoundException("Entity not Found with this id");

        return review;
    }
    public async Task<int> CreateAsync(ReviewDtoBase dto)
    {
        var reviewId = await _repository.CreateAsync(new()
        {
            Message = dto.Message,
            ProductId = dto.ProductId,
            Rate = dto.Rate,
            UserId = dto.UserId
        });

        return reviewId;
    }
    public async Task<int> UpdateAsync(ReviewUpdateDto dto)
    {
        var reviewId = await _repository.UpdateAsync(new()
        {
            Id = dto.Id,
            Message = dto.Message,
            ProductId = dto.ProductId,
            Rate = dto.Rate,
            UserId = dto.UserId
        });

        return reviewId;
    }
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);

    }




}
