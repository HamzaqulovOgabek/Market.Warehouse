using AutoMapper;
using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.DataAccess.Repository.CategoryRepository;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        this._mapper = mapper;
    }   

    public IQueryable<CategoryListDto> GetList(BaseSortFilterDto options)
    { 
        return _repository.GetAll()
             .SortFilter(options)
             .Select(x => _mapper.Map<CategoryListDto>(x));
    }
    public async Task<CategoryDto> GetAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
            throw new EntityNotFoundException("Category not found with this id");

        return _mapper.Map<CategoryDto>(category);
    }
    public async Task<int> CreateAsync(CategoryDtoBase dto)
    {
        var category = _mapper.Map<Category>(dto);
       return await _repository.CreateAsync(category);
    }
    public async Task<int> UpdateAsync(CategoryUpdateDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        return await _repository.UpdateAsync(category);
    }
    public async Task CategorizeProductAsync(int productId, int categoryId)
    {
        await _repository.CategorizeProductAsync(productId, categoryId);
    }
    public async Task Delete(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
