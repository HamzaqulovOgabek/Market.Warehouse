namespace E_CommerceProjectDemo.Application.Services.CategoryServices;

public class CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ProductCount { get; set; }
    public int ParentId { get; set; }
    public List<CategoryDto>? SubCategories { get; set; }
    //public List<ProductCreateDto>? Products { get; set; }
}
