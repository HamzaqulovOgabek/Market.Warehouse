using Market.Warehouse.DataAccess.Context;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.Domain.Enums;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.DataAccess.Repository;

public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TId : struct
    where TEntity : BaseEntity<TId>, IHaveState
{
    protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> GetAll()
    {
        return Context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
    public async Task<TId> CreateAsync(TEntity entity)
    {
        var entry = Context.Add(entity);
        entry.State = EntityState.Added;
        if (entity != null && entity is Auditable<TId>)
        {
            var auditableEntity = entity as Auditable<TId>;
            auditableEntity!.CreatedAt = DateTime.Now;
        }
        await Context.SaveChangesAsync();
        return entry.Entity.Id;
    }
    public async Task<TId> UpdateAsync(TEntity entity)
    {
        var entry = Context.Entry(entity);
        entry.State = EntityState.Modified;
        if (entity != null && entity is Auditable<TId>)
        {
            Auditable<TId>? auditableEntity = entity as Auditable<TId>;
            auditableEntity.ModifiedAt = DateTime.Now;
        }
        await Context.SaveChangesAsync();
        return entry.Entity.Id;
    }
    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            throw new EntityNotFoundException($"Entity with id {id} not found.");
        }
        if(entity is IHaveState haveState)
        {
            entity.State = State.PASSIVE;
        }
        await Context.SaveChangesAsync();
    }
}
