using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.DataAccess.SeedData;

namespace Market.Warehouse.Application.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var seeders = serviceScope.ServiceProvider.GetServices<AppDbContext>();

        string jsontext = File.ReadAllText("SeedData/Products.json");
        Seeding.SeedAsync(jsontext, app.ApplicationServices).GetAwaiter().GetResult();

        return app;

    }

}