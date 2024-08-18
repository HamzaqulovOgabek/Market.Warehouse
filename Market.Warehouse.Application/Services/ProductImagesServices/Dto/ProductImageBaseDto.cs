using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.ViewModel.ProductImage
{
    public class ProductImageBaseDto
    {
        public required string ImageUrl { get; set; }
        public required int ProductId { get; set; }
    }
}
