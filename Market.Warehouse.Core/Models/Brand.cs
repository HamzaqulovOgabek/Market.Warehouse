using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(Brand))]
public class Brand : BaseEntity<int>
{
    [RegularExpression("^[\\w1-9._]{3,50}$")]
    public required string Name { get; set; }    
    public ICollection<Product>? Products { get; set; }

}