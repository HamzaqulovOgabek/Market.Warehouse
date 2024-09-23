using Market.Warehouse.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(Review))]
public class Review : Auditable<int>, IHaveState
{
    public required string Message { get; set; }
    public required int Rate { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public State State { get; set; } = State.ACTIVE;

    public Product? Product { get; set; }
    
}
