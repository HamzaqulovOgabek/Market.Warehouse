using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository.DiscountRepository;

public class DiscountRepository : BaseRepository<Discount, int>, IDiscountRepository
{
    private readonly AppDbContext _context;

    public DiscountRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

}
