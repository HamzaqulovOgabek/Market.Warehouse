using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Warehouse.Domain.Models;

[Table(nameof(Favourite))]
public class Favourite : CartItem
{
}
