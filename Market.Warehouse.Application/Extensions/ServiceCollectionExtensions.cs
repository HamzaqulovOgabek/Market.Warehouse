using E_CommerceProjectDemo.Application.Services.CartServices;
using Market.Warehouse.Application.Services.BrandServices;
using Market.Warehouse.Application.Services.CategoryServices;
using Market.Warehouse.Application.Services.DiscountServices;
using Market.Warehouse.Application.Services.ProductImagesServices;
using Market.Warehouse.Application.Services.ProductServices;
using Market.Warehouse.Application.Services.RedisCacheServices;
using Market.Warehouse.Application.Services.ReviewServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Market.Warehouse.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration["Redis:ConnectionString"];
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<IRedisCacheService, RedisCacheService>();

    }

}