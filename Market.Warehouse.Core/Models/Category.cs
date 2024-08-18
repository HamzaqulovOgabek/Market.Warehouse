using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(Category))]
public class Category : BaseEntity<int>, IHaveState
{
    public required string Name { get; set; }
    public int? ParentId { get; set; }
    public State State { get; set; } = State.ACTIVE;

    [ForeignKey(nameof(ParentId))]
    public Category? Parent { get; set; }
    public ICollection<Product>? Products { get; set; }
}
