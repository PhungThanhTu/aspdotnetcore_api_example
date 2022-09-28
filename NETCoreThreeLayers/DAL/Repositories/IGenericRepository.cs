using System.Linq.Expressions;

namespace DAL.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    public IQueryable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int numTake = 0
        );

    TEntity? GetById(object id);

    public IEnumerable<TEntity> GetAll();

    public TEntity Insert(TEntity entityToInsert);

    public TEntity Update(TEntity entityToUpdate);

    public void Delete(TEntity entityToDelete);

    public void Delete(object id);
}