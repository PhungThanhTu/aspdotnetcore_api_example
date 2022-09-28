using DAL;
using DAL.Configs;
using Microsoft.EntityFrameworkCore;

IConfig config = new MSSQLDbConfig();

RepositoryContext dbContext = new RepositoryContext(config);

dbContext.Database.EnsureCreated();

Console.WriteLine(dbContext.Database.GetDbConnection().Database);