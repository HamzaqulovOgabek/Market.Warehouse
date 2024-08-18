using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;
[Table(nameof(Warehouse))]
public class Warehouse : BaseEntity<int>
{
    public required string Name { get; set; }
    public required string Location { get; set; }
    //public ICollection<Product>? Products { get; set; }
}
