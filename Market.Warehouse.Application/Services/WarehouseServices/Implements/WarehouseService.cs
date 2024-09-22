using AutoMapper;
using Market.Warehouse.Application.Dto;
using Market.Warehouse.Application.Extensions;
using Market.Warehouse.Application.Services.ProductServices;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.DataAccess.Repository.WarehouseRepository;

namespace Market.Warehouse.Application.Services.WarehouseServices;

public class WarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IMapper _mapper;

    public WarehouseService(IWarehouseRepository warehouseRepository, IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _mapper = mapper;
    }

    // Implement Warehouse-specific methods here
    public async Task<WarehouseDto> GetWarehouseAsync(int warehouseId)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId);
        return _mapper.Map<WarehouseDto>(warehouse);
    }
    public IQueryable<WarehouseDto> GetAllWarehouses(BaseSortFilterDto dto)
    {
        var warehouses = _warehouseRepository.GetAll().SortFilter(dto);
        // Apply sorting and filtering logic based on the provided dto
        // Example: return warehouses.OrderBy(w => w.Name).AsQueryable();
        return _mapper.ProjectTo<WarehouseDto>(warehouses);
    }
    public async Task AddWarehouseAsync(WarehouseDto warehouseDto)
    {
        var warehouse = _mapper.Map<Domain.Models.Warehouse>(warehouseDto);
        await _warehouseRepository.CreateAsync(warehouse);
    }
    public async Task UpdateWarehouseAsync(WarehouseDto dto)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(dto.Id);
        if (warehouse != null)
        {
            _mapper.Map(dto, warehouse);
            await _warehouseRepository.UpdateAsync(warehouse);
        }
        else
        {
            throw new EntityNotFoundException("Warehouse not found");
        }
    }
    public async Task RemoveWarehouseAsync(int warehouseId)
    {
        await _warehouseRepository.DeleteAsync(warehouseId);
    }
}
