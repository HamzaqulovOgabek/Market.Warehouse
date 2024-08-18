using AutoMapper;
using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.DataAccess.Repository.BrandRepository;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.BrandServices;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IQueryable<BrandDtoBase> GetAll(BaseSortFilterDto dto)
    {

        var query = _repository.GetAll()
            .SortFilter(dto, nameof(BrandDto.Name))
            .Select(x => _mapper.Map<BrandDtoBase>(x));

        return query;
    }
    public async Task<BrandDto> GetByIdAsync(int id)
    {
        var brand = await _repository.GetByIdAsync(id);

        if (brand == null)
        {
            throw new EntityNotFoundException("Brand not found by this id");
        }
        return _mapper.Map<BrandDto>(brand);
    }
    public async Task<int> CreateAsync(BrandDtoBase dto)
    {
        var brand = _mapper.Map<Brand>(dto);
        var entityId = await _repository.CreateAsync(brand);

        return entityId;
    }
    public async Task<int> UpdateAsync(BrandDto dto)
    {
        var entity = _mapper.Map<Brand>(dto);
        var entityId = await _repository.UpdateAsync(entity);

        return entityId;
    }
    public async Task DeleteAsync(int id)
    {

        await _repository.DeleteAsync(id);
    }
}