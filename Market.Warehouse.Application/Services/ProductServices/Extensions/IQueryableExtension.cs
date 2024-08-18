using Market.Warehouse.Application.Extensions;

namespace Market.Warehouse.Application.Services.ProductServices;

public static class IQueryableExtension
{
    public static IQueryable<ProductListDto> SortFilter(
        this IQueryable<ProductListDto> query,
        ProductSortFilterDto options)
    {
        if (options.DiscountId.HasValue)
            query = query.Where(x => x.DiscountId == options.DiscountId.Value);

        if (options.BrandId.HasValue)
            query = query.Where(x => x.BrandId == options.BrandId.Value);

        if (options.CategoryId.HasValue)
            query = query.Where(x => x.CategoryId == options.CategoryId.Value);

        if (options.FromPrice.HasValue)
            query = query.Where(x => x.Price >= options.FromPrice.Value
            || x.DiscountPrice >= options.FromPrice);

        if (options.ToPrice.HasValue)
            query = query.Where(x => x.Price <= options.ToPrice.Value
            || x.DiscountPrice <= options.ToPrice.Value);

        query = query.SortFilter(options,
            nameof(ProductListDto.Name),
            nameof(ProductListDto.Color),
            nameof(ProductListDto.Price),
            nameof(ProductListDto.Features)
            );
        return query;
    }
}
