using DAL.Aggregates;
using Json.Net;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configs;

public class MSSQLDbConfig : IConfig
{
    public MSSQLDbConfig(string json)
    {
        var data = JsonNet.Deserialize<dynamic>(json);
        Server = data.Server;
        Username = data.Username;
        Password = data.Password;
        Database = data.Database;
    }

    public MSSQLDbConfig()
    {
        Server = "127.0.0.1,1433";
        Username = "SA";
        Password = "Superadmin123456!1";
        Database = "student_app_db";
    }

    public string Server { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }

    public string GetConnectionString()
    {
        return $@"
            Server={Server};
            User Id={Username};
            Database={Database};
            Password={Password}
        ";
    }

    public DbContextOptionsBuilder UseDBMS(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        return optionsBuilder.UseSqlServer(GetConnectionString());
    }

    public void OnModelCreating(ModelBuilder modelBuilder)
    {


        var userHasTask = new User { Username = "sample4", Password = "sampass4", Email = "sample4@sample.sample", FullName = "Sample User Four", Id = Guid.NewGuid() };
        var admin = new User { Username = "sudo", Password = BCrypt.Net.BCrypt.HashPassword("sudo"), Email = "admin@pro.org", FullName = "Super User Admin", Id = Guid.NewGuid() };



        modelBuilder.Entity<User>().HasData(
            admin,
            userHasTask
            );


    }
}