using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(Discount))]
public class Discount : Auditable<int>, IHaveState
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required DateTime ExpirationDate { get; set; }
    [Range(5, 100, ErrorMessage = "Interest rate should be in range of [5, 100]")]
    public required decimal InterestRate { get; set; }
    public State State { get; set; } = State.ACTIVE;
    public ICollection<Product>? Products { get; set; }
}