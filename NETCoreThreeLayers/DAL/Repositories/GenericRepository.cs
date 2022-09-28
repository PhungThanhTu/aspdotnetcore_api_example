using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DAL.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public void Delete(TEntity entityToDelete)
    {
        _dbSet.Remove(entityToDelete);
    }

    public void Delete(object id)
    {
        TEntity? entityToDelete = _dbSet.Find(id);
        if (entityToDelete != null)
            _dbSet.Remove(entityToDelete);
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity,
                                    bool>>? filter = null, Func<IQueryable<TEntity>,
                                    IOrderedQueryable<TEntity>>? orderBy = null,
                                    int numTake = 0)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null) query = query.Where(filter);

        if (orderBy != null)
        {
            return orderBy(query).Take(numTake);
        }
        else
        {
            return numTake > 0 ? query.Take(numTake) : query;
        }
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public TEntity? GetById(object id)
    {
        var entity = _dbSet.Find(id);
        if (entity is not null) 
            _dbContext.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public TEntity Insert(TEntity entityToInsert)
    {
        _dbSet.Add(entityToInsert);
        return entityToInsert;
    }

    public TEntity Update(TEntity entityToUpdate)
    {
        _dbContext.Update(entityToUpdate);
        return entityToUpdate;
    }
}