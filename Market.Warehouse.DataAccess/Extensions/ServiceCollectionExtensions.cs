using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.DataAccess.Repository.BrandRepository;
using Market.Warehouse.DataAccess.Repository.CartItemRepository;
using Market.Warehouse.DataAccess.Repository.CartRepository;
using Market.Warehouse.DataAccess.Repository.CategoryRepository;
using Market.Warehouse.DataAccess.Repository.DiscountRepository;
using Market.Warehouse.DataAccess.Repository.InventoryRepository;
using Market.Warehouse.DataAccess.Repository.ProductImagesRepository;
using Market.Warehouse.DataAccess.Repository.ProductRepository;
using Market.Warehouse.DataAccess.Repository.ReviewRepository;
using Market.Warehouse.DataAccess.Repository.WarehouseRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Warehouse.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Market.WareHouse"));
        });
        AddRepositories(services);
        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IProductImagesRepository, ProductImagesRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();


    }


}
