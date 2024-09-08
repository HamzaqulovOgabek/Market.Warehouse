namespace E_CommerceProjectDemo.Application.Services.CategoryServices;

public class CategoryListDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ProductCount { get; set; }
}
