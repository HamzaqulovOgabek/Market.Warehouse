using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.DataAccess.SeedData;

public class Seeding
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        await SeedCategory(context);

        await SeedBrand(context);

        await SeedProduct(context);

        await SeedWarehouse(context);

        await SeedInventory(context);

        await SeedReviewForProduct(context);

    }


    private static async Task SeedCategory(AppDbContext? context)
    {
        IsAppDbContextNull(context);

        int index = 1;
        var categories = new List<Category>();

        while (index <= 50)
        {
            var category = new Category
            {
                Name = $"Category {index}",
                ParentId = new Random().Next(1, 11),
            };
            categories.Add(category);
            index++;
        }
        if (!await context.Categories.AnyAsync())
        {
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

    }
    private static async Task SeedBrand(AppDbContext? context)
    {
        IsAppDbContextNull(context);

        int index = 1;
        var brands = new List<Brand>();
        while (index < 100)
        {
            var brand = new Brand
            {
                Name = $"Brand {index}",
            };
            brands.Add(brand);
            index++;
        }
        if (!await context.Brands.AnyAsync())
        {
            await context.Brands.AddRangeAsync(brands);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedProduct(AppDbContext? context)
    {
        IsAppDbContextNull(context);

        int index = 1;
        var products = new List<Product>();
        while (index < 2000)
        {
            var product = new Product
            {
                Name = $"Product {index}",
                Description = "Description for Product {index}",
                Price = index,
                CategoryId = new Random().Next(1, 51),
                BrandId = new Random().Next(1, 100)
            };
            products.Add(product);
            index++;
        }
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedWarehouse(AppDbContext? context)
    {
        IsAppDbContextNull(context);

        int index = 1;
        var warehouses = new List<Domain.Models.Warehouse>();
        while (index < 4)
        {
            var inventory = new Domain.Models.Warehouse
            {
                Name = $"Warehouse {index}",
                City = $"City {index}",
                Location = $"Location {index}",
                Street = $"Street {index}",
                PostalCode = new Random().Next(10000, 99999).ToString(),
            };
            warehouses.Add(inventory);
            index++;
        }
        if (!await context.WareHouses.AnyAsync())
        {
            await context.WareHouses.AddRangeAsync(warehouses);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedInventory(AppDbContext? context)
    {
        IsAppDbContextNull(context);

        int index = 1;
        var inventories = new List<Inventory>();
        while (index < 2000)
        {
            var inventory = new Inventory
            {
                ProductId = new Random().Next(1, 2000),
                WarehouseId = new Random().Next(1, 4),
                Quantity = new Random().Next(1, 100),
            };
            inventories.Add(inventory);
            index++;
        }
         if (!await context.Inventories.AnyAsync())
        {
            await context.Inventories.AddRangeAsync(inventories);
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedReviewForProduct(AppDbContext? context)
    {
        IsAppDbContextNull(context);

        int index = 1;
        var reviews = new List<Review>();
        while (index < 2000)
        {
            var inventory = new Review
            {
                ProductId = new Random().Next(1, 1000),
                Message = $"Review {index}",
                Rate = new Random().Next(2, 6)
            };
            reviews.Add(inventory);
            index++;
        }
         if (!await context.Reviews.AnyAsync())
        {
            await context.Reviews.AddRangeAsync(reviews);
            await context.SaveChangesAsync();
        }
    }
    private static void IsAppDbContextNull(AppDbContext? context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
    }
}
