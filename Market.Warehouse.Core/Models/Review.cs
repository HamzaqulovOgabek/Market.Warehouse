using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(Review))]
public class Review : Auditable<int>
{
    public required string Message { get; set; }
    public int Rate { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }

    public Product? Product { get; set; }
    
}
