using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Market.Warehouse.DataAccess.SeedData
{
    public class Seeding
    {
        public static async Task SeedAsync(string jsonData, IServiceProvider serviceProvider)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
            var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            if(! await context.Products.AnyAsync())
            {
                await context.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
