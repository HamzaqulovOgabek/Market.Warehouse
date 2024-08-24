using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(Brand))]
public class Brand : BaseEntity<int>, IHaveState
{
    [RegularExpression("^[\\w1-9._]{3,50}$")]
    public required string Name { get; set; }
    public State State { get; set; } = State.ACTIVE;

    public ICollection<Product>? Products { get; set; }

}