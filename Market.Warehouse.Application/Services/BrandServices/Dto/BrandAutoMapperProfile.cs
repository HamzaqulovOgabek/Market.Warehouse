using AutoMapper;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.BrandServices;

public class BrandAutoMapperProfile : Profile
{
    public BrandAutoMapperProfile()
    {
        CreateMap<BrandDto, BrandDtoBase>();
        CreateMap<BrandDtoBase, Brand>();
        CreateMap<Brand, BrandDto>();
        CreateMap<BrandDto, Brand>();

        //Add other mappings here
    }
}
