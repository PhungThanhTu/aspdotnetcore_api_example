using Microsoft.EntityFrameworkCore;

namespace DAL.Configs;

public interface IConfig
{
    string GetConnectionString();

    DbContextOptionsBuilder UseDBMS(DbContextOptionsBuilder optionsBuilder);

    void OnModelCreating(ModelBuilder modelBuilder);
}