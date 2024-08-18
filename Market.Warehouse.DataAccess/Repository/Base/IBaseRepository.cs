
using Market.Warehouse.Domain.Models;

namespace Market.Warehouse.DataAccess.Repository;

public interface IBaseRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    Task<TId> CreateAsync(TEntity entity);
    Task DeleteAsync(TId id);
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(TId id);
    Task<TId> UpdateAsync(TEntity entity);
}
