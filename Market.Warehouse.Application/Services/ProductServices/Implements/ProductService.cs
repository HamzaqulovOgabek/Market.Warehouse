using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public ProductService(IProductRepository repository, IMapper mapper, IRedisCacheService redisCacheService)
    {
        _repository = repository;
        _mapper = mapper;
        _redisDatabase = redisCacheService;
    }
    public async Task<ProductDto> Get(int id)
    {
        string cacheKey = $"product:{id}";
        var cachedProduct = await _redisDatabase.GetCacheAsync<Product>(cacheKey);
        if (cachedProduct != null)
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

        IsValidData(dto);

        var productId = await _repository.CreateAsync(product);
        return productId;
    }
    public async Task<int> AddAListOfProducts(List<ProductBaseDto> dtos)
    {
        var products = _mapper.Map<List<Product>>(dtos);
        await _repository.AddAListOfProducts(products);

        return products.Count;
    }
    public async Task<int> Update(ProductDto dto)
    {
        IsValidData(dto);
        var product = _mapper.Map<Product>(dto);
        var productId = await _repository.UpdateAsync(product);
        return productId;
    }
    public async Task Delete(int id)
    {
        // do we such a product
        await Get(id);
        await _repository.DeleteAsync(id);
    }
    private static void IsValidData(ProductBaseDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Price <= 0)
        {
            throw new InvalidOperationException("Invalid data for product");
        }
    }

}