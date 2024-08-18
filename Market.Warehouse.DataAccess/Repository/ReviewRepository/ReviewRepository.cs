using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository.ReviewRepository;

public class ReviewRepository : BaseRepository<Review, int>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
        
    }
}
