using DAL;
using DAL.Configs;
using DAL.RepositoriesManager;
using Microsoft.Extensions.Configuration;

namespace BLL.Common;

public class SharedRepositories : ISharedRepositories
{
    private readonly IConfiguration _configuration;
    private readonly RepositoriesManager _repositoryManager;

    public SharedRepositories(IConfiguration configuration)
    {
        _configuration = configuration;
        _repositoryManager = new RepositoriesManager(new RepositoryContext(new MSSQLDbConfig()
        {
            Server = _configuration["Database:Server"],
            Database = _configuration["Database:Database"],
            Username = _configuration["Database:Username"],
            Password = _configuration["Database:Password"]
        }));
    }

    public IRepositoriesManager RepositoriesManager { get => _repositoryManager; }
}