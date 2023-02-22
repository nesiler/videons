using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Videons.Core.Entities;

namespace Videons.Core.DataAccess.EntityFramework;

public abstract class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : EntityBase, new()
    where TContext : DbContext
{
    protected readonly TContext Context;

    protected EfEntityRepositoryBase(TContext context)
    {
        Context = context;
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        return Context.Set<TEntity>().SingleOrDefault(filter);
    }

    public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
    {
        return filter == null
            ? Context.Set<TEntity>().ToList()
            : Context.Set<TEntity>().Where(filter).ToList();
    }

    public bool Add(TEntity entity)
    {
        entity.CreatedAt = DateTime.Now.ToUniversalTime();

        var entry = Context.Entry(entity);
        entry.State = EntityState.Added;

        return Context.SaveChanges() > 0;
    }

    public bool Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.Now.ToUniversalTime();

        var entry = Context.Entry(entity);
        entry.State = EntityState.Modified;

        return Context.SaveChanges() > 0;
    }

    public bool Delete(TEntity entity)
    {
        var entry = Context.Entry(entity);

        entry.State = EntityState.Deleted;

        return Context.SaveChanges() > 0;
    }
}