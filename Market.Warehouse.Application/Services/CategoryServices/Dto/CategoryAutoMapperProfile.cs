using AutoMapper;
using E_CommerceProjectDemo.Application.Services.CategoryServices;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.CategoryServices;

public class CategoryAutoMapperProfile : Profile
{
    public CategoryAutoMapperProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(d => d.ProductCount, cfg => cfg.MapFrom(e => e.Products!.Count));
        CreateMap<Category, CategoryListDto>()
            .ForMember(d => d.ProductCount, cfg => cfg.MapFrom(e => e.Products.Count));
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
    }
}