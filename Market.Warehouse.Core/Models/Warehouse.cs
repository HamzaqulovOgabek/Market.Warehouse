using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;
[Table(nameof(Warehouse))]
public class Warehouse : Auditable<int>, IHaveState
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string? PostalCode { get; set; }
    public State State { get; set; } = State.ACTIVE;
    public ICollection<Product>? Products { get; set; }
}
