using AutoMapper;
using AutoMapper.QueryableExtensions;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.Application.Services.RedisCacheServices;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.DataAccess.Repository.ProductRepository;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _redisDatabase;

    public ProductService(IProductRepository repository, IMapper mapper, IRedisCacheService redisDatabase)
    {
        _repository = repository;
        _mapper = mapper;
        _redisDatabase = redisDatabase;
    }

    public async Task<ProductDto> Get(int id)
    {
        string cacheKey = $"product:{id}";
        var cachedProduct = await _redisDatabase.GetCacheAsync<Product>(cacheKey);
        if(cachedProduct != null)
        {
            return _mapper.Map<ProductDto>(cachedProduct);
        }

        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            throw new EntityNotFoundException("Entity not Found with this id");

        await _redisDatabase.SetCacheAsync(cacheKey, product, TimeSpan.FromMinutes(30));
        return _mapper.Map<ProductDto>(product);
    }
    public IQueryable<ProductListDto> GetList(ProductSortFilterDto options)
    {
        var products = _repository.GetAll()
            .Include(x => x.Discount)
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