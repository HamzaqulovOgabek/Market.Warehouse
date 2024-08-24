using AutoMapper;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.BrandServices;

public class BrandAutoMapperProfile : Profile
{
    public BrandAutoMapperProfile()
    {
        CreateMap<Brand, BrandDtoBase>();
        CreateMap<Brand, BrandDto>().ReverseMap();
    }
}
