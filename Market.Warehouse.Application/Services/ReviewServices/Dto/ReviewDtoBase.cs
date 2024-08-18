namespace Market.Warehouse.Application.Services.ReviewServices;

public class ReviewDtoBase
{
    public required string Message { get; set; }
    public int Rate { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
}
