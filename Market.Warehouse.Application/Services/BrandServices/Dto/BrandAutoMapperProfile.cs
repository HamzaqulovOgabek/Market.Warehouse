using AutoMapper;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.BrandServices;

public class BrandAutoMapperProfile : Profile
{
    public BrandAutoMapperProfile()
    {
        CreateMap<Brand, BrandDtoBase>().ReverseMap();
        CreateMap<Brand, BrandDto>()
            .ForMember(d => d.ProductCount, cfg => cfg.MapFrom(e => e.Products.Count)).ReverseMap();
    }
}
