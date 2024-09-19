using AutoMapper;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.Application.Services.InventoryServices;

public class InventoryAutoMapperProfile : Profile
{
    public InventoryAutoMapperProfile()
    {
        CreateMap<Inventory, InventoryDto>();
        
    }
}
