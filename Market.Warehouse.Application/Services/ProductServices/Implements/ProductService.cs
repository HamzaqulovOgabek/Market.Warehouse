using AutoMapper;
using AutoMapper.QueryableExtensions;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.DataAccess.Repository.ProductRepository;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        this._mapper = mapper;
    }

    public async Task<ProductDto> Get(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            throw new EntityNotFoundException("Entity not Found with this id");

        return _mapper.Map<ProductDto>(product);
    }
    public IQueryable<ProductListDto> GetList(ProductSortFilterDto options)
    {
        var products = _repository.GetAll()
            .Include(x => x.Discount)
            //.Select(x => _mapper.Map<ProductListDto>(x))
            .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
            .SortFilter(options);

        return products;
    }
    public async Task<int> Create(ProductBaseDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        if (await _repository.ExistsByNameAsync(dto.Name))
            throw new Exception($"Product with the {nameof(dto.Name)} already exists");

        var productId = await _repository.CreateAsync(product);
        return productId;
    }
    public async Task<int> Update(ProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        var productId = await _repository.UpdateAsync(product);
        return productId;
    }
    public async Task Delete(int id)
    {
        await _repository.DeleteAsync(id);
    }
}