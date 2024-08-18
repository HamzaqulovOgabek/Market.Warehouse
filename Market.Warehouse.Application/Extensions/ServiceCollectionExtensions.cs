using Market.Warehouse.Application.Services.BrandServices;
using Market.Warehouse.Application.Services.CategoryServices;
using Market.Warehouse.Application.Services.DiscountServices;
using Market.Warehouse.Application.Services.ProductServices;
using Market.Warehouse.Application.Services.ReviewServices;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Warehouse.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BrandAutoMapperProfile));
        services.AddAutoMapper(typeof(CategoryMappingProfile));
        services.AddAutoMapper(typeof(ProductMappingProfile));
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
    }
}