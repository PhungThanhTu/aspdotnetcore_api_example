using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.RepositoriesManager;

public interface IRepositoriesManager : IDisposable
{
    IGenericRepository<User> UserRepository { get; }
 
    DbContext Context { get; }

    public void Saves();

    public void Dispose(bool disposing);
}