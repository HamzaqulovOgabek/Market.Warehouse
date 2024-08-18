using AutoMapper;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        var now = DateTime.Now;
        CreateMap<ProductBaseDto, Product>();
        //CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductListDto>()
            .ForMember(d => d.RewierCount, cfg => cfg.MapFrom(e => e.Reviews!.Count))
            .ForMember(d => d.Discount, cfg => cfg.MapFrom(e => e.Discount!.InterestRate.FormattedToString() + " %"))
            .ForMember(d => d.DiscountPrice, cfg => cfg.MapFrom(e =>
                    //e.Discount.ExpirationDate < now
                    //?
                    e.Price - (e.Discount!.InterestRate / 100) * e.Price)
            );

    }
}
