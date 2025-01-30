using Domain.Abstractions;

namespace Domain.Repositories.Base;

public interface IRepository<TEntity, in TKey>
    where TEntity : IEntityBase<TKey>
{
    IQueryable<TEntity> GetQueryable();
    ValueTask<TEntity?> FindAsync(TKey id);

    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}