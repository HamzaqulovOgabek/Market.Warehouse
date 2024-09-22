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
    }
    private static async Task SeedCategory(AppDbContext? context)
    {
        if(context == null) throw new ArgumentNullException(nameof(context));

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
    private static async Task SeedInventory(AppDbContext? context)
    {
        int index = 1;
        var inventories = new List<Inventory>();
        while (index < 2000)
        {
            var inventory = new Inventory
            {
                ProductId = new Random().Next(1, 2000),
                WarehouseId = new Random().Next(1, 10),
                Quantity = new Random().Next(1, 100)
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

}
