using DAL.Aggregates;
using Microsoft.EntityFrameworkCore;
using BCrypt;

namespace DAL.Configs;

public class InMemoryDBConfig : IConfig
{
    public string GetConnectionString()
    {
        return "";
    }

    public void OnModelCreating(ModelBuilder modelBuilder)
    {
      

        modelBuilder.Entity<User>().HasData(
            new User { Username = "sample", Password = "sampass", Email = "sample@sample.sample", FullName = "Sample User", Id = new("3eaaabc4-746f-47f9-820a-54ad2c4660dd") }

            );
        modelBuilder.Entity<User>().HasData(
            new User { Username = "sample2", Password = "sampass2", Email = "sample2@sample.sample", FullName = "Sample User Two", Id = new("5c87461c-8613-425f-89b3-83c7b741361e") }
            );
        modelBuilder.Entity<User>().HasData(
            new User { Username = "sample3", Password = "sampass3", Email = "sample3@sample.sample", FullName = "Sample User Three", Id = new("270d8311-f96d-42b9-a62c-73e01c120d11") }
            );
        modelBuilder.Entity<User>().HasData(
            new User { Username = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Email = "admin@admin.vn", FullName = "Sample Admin", Id = Guid.NewGuid() }
            );

    }

    public DbContextOptionsBuilder UseDBMS(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        return optionsBuilder.UseInMemoryDatabase("test_db");
    }
}