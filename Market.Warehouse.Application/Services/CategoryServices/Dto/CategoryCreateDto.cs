using System.ComponentModel.DataAnnotations;

namespace E_CommerceProjectDemo.Application.Services.CategoryServices;

public class CategoryCreateDto
{
    [MaxLength(255)]
    public required string Name { get; set; }
    public int? ParentId { get; set; }

}
