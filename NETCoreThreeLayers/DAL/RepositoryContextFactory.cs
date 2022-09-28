using DAL.Configs;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    private readonly IConfig _config;

    public RepositoryContextFactory()
    {
        _config = new MSSQLDbConfig();
    }

    public RepositoryContext CreateDbContext(string[] args)
    {
        return new RepositoryContext(_config);
    }
}