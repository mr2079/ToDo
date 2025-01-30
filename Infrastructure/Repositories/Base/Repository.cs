using Domain.Abstractions;
using Domain.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(AppDbContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return DbSet.AsQueryable();
    }

    public ValueTask<TEntity?> FindAsync(TKey id)
    {
        return DbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }
}