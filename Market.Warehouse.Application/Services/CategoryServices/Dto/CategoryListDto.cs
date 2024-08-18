using Market.Warehouse.Domain.Enums;

namespace Market.Warehouse.Application.Services.CategoryServices;

public record CategoryListDto(
    int Id,
    string Name,
    State State) : CategoryDtoBase(Name);

