using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.DataAccess.Repository.DiscountRepository;

namespace Market.Warehouse.Application.Services.DiscountServices;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _repository;

    public DiscountService(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiscountDto> Get(int id)
    {
        var discount = await _repository.GetByIdAsync(id);
        if (discount == null)
            throw new EntityNotFoundException("Discount not found with this id");

        return new DiscountDto
        {
            Id = discount.Id,
            Name = discount.Name,
            InterestRate = discount.InterestRate,
            Code = discount.Code,
            ExpirationDate = discount.ExpirationDate,
            ProductCount = discount.Products == null ? 0 : discount.Products.Count
        };
    }
    public IQueryable<DiscountUpdateDto> GetList(BaseSortFilterDto options)
    {
        var discounts = _repository.GetAll()
            .SortFilter(options)
            .Select(x => new DiscountUpdateDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                InterestRate = x.InterestRate,
                ExpirationDate = x.ExpirationDate
            });
        if (discounts == null)
            throw new EntityNotFoundException("Discount not found with this id");

        return discounts;
    }
    public async Task<int> Create(DiscountBaseDto discount)
    {
        var creaetedDiscountId = await _repository.CreateAsync(new()
        {
            Name = discount.Name,
            Code = discount.Code,
            ExpirationDate = discount.ExpirationDate,
            InterestRate = discount.InterestRate,
        });

        return creaetedDiscountId;
    }

    public async Task<int> Update(DiscountUpdateDto dto)
    {
        var updatedDiscountId = await _repository.UpdateAsync(new()
        {
            Id = dto.Id,
            Code = dto.Code,
            Name = dto.Name,
            ExpirationDate = dto.ExpirationDate,
            InterestRate = dto.InterestRate,
            State = dto.State
        });

        return updatedDiscountId;
    }
    public async Task Delete(int id)
    {
        await _repository.DeleteAsync(id);

    }

}
