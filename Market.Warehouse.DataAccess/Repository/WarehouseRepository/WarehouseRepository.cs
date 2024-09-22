using Market.Warehouse.DataAccess.Context;

namespace Market.Warehouse.DataAccess.Repository.WarehouseRepository
{
    public class WarehouseRepository : BaseRepository<Domain.Models.Warehouse, int>, IWarehouseRepository
    {
        public WarehouseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
