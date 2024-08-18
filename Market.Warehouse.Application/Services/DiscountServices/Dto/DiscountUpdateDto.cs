using Market.Warehouse.Domain.Enums;

namespace Market.Warehouse.Application.Services.DiscountServices;

public class DiscountUpdateDto : DiscountBaseDto
{
    public int Id { get; set; }
    public State State { get; set; }

}
