using AutoMapper;
using E_CommerceProjectDemo.Application.Services.ProductServices;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.Application.Services.DiscountServices;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.ProductServices;

public class ProductAutoMapperProfile : Profile
{
    public ProductAutoMapperProfile()
    {
        var now = DateTime.Now;
        CreateMap<ProductBaseDto, Product>();
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.BrandName, cfg => cfg.MapFrom(e => e.Brand.Name))
            .ForMember(d => d.DiscountPrice, cfg => cfg.MapFrom(e =>
                    e.Discount.ExpirationDate < now
                    ?
                    e.Price - (e.Discount!.InterestRate / 100) * e.Price : 0)
            )
            .ForMember(d => d.ReviewCount, cfg => cfg.MapFrom(e => e.Reviews.Count))
            .ForMember(d => d.Rating,
            cfg => cfg.MapFrom(e => e.Reviews.Any() ? (double)e.Reviews.Sum(r => r.Rate) / e.Reviews.Count : 0))
            .ForMember(d => d.DiscountDto, cfg => cfg.MapFrom(e => e.Discount))
            .ForMember(d => d.ReviewDtos, cfg => cfg.MapFrom(e => e.Reviews));

        CreateMap<Discount, DiscountDto>();
        CreateMap<Review, ReviewDto>();

        CreateMap<Product, ProductListDto>()
            .ForMember(d => d.DiscountPrice,
            cfg => cfg.MapFrom(e =>
                e.Discount.ExpirationDate > now ?
                e.Price - e.Price * e.Discount.InterestRate / 100 : e.Price))
            .ForMember(d => d.ReviewCount, cfg => cfg.MapFrom(e => e.Reviews.Count))
            .ForMember(d => d.Rating,
            cfg => cfg.MapFrom(e =>
                e.Reviews.Any() ? e.Reviews.Average(e => e.Rate) : 0));

    }
}
