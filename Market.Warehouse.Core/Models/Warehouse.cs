using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;
[Table(nameof(Warehouse))]
public class Warehouse : BaseEntity<int>, IHaveState
{
    public required string Name { get; set; }
    public required string Location { get; set; }
    public State State { get; set; } = State.ACTIVE;
    public ICollection<Product>? Products { get; set; }
}
