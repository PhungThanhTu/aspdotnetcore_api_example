using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.RepositoriesManager;

public class RepositoriesManager : IRepositoriesManager
{
    private readonly DbContext _dbContext;
    private readonly IGenericRepository<User> _userRepository;


    public RepositoriesManager(DbContext dbContext)
    {
        _dbContext = dbContext;
        _userRepository = new GenericRepository<User>(dbContext);

    }

    public void Saves()
    {
        _dbContext.SaveChanges();
    }

    private bool disposed = false;

    public IGenericRepository<User> UserRepository => _userRepository;



    public DbContext Context => _dbContext;

    public void Dispose(bool disposing)
    {
        if (!this.disposed && disposing)
        {
           
                _dbContext.Dispose();
            
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}