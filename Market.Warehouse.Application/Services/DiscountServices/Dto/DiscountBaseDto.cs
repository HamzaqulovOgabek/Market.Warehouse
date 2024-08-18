using Market.Warehouse.Domain.Enums;

namespace Market.Warehouse.Application.Services.DiscountServices;

public class DiscountBaseDto
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public decimal InterestRate { get; set; }
    public DateTime ExpirationDate { get; set; }
}
