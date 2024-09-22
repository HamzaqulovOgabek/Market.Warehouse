
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Market.Warehouse.DataAccess.Repository;

public interface IBaseRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>
{
    IDbContextTransaction BeginTransaction();
    Task<TId> CreateAsync(TEntity entity);
    Task DeleteAsync(TId id);
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(TId id);
    Task RollBackTransactionAsync();
    Task<TId> UpdateAsync(TEntity entity);
    Task СommitTransactionAsync();
}
