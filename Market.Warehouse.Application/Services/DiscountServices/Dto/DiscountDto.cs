using Market.Warehouse.Domain.Enums;

namespace Market.Warehouse.Application.Services.DiscountServices;

public class DiscountDto : DiscountBaseDto
{
    public int Id { get; set; }
    public int ProductCount { get; set; }
}
