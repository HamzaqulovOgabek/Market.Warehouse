using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.DataAccess.SeedData;

namespace Market.Warehouse.Application.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        
        Seeding.SeedAsync(app.ApplicationServices).GetAwaiter().GetResult();

        return app;
    }
}