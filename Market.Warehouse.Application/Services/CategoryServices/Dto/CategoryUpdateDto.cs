namespace Market.Warehouse.Application.Services.CategoryServices.Dto;

public class CategoryUpdateDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentId { get; set; }
}
