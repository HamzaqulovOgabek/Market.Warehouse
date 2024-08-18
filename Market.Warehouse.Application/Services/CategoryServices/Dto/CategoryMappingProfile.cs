using AutoMapper;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.CategoryServices;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(d => d.ProductCount, cfg => cfg.MapFrom(e => e.Products!.Count));
        CreateMap<Category, CategoryListDto>();
    }
}