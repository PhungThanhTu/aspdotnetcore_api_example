using System.IO;
using Microsoft.EntityFrameworkCore;
using NetCoreAPIExample.DAL;
using NetCoreAPIExample.DAL.Configs;
using static System.Console;



var config = new MSSQLDbConfig()
{
    Server = "127.0.0.1,1433",
    Username = "SA",
    Password = "Superadmin123456!1",
    Database = "student_app_db"
};

var db = new AppDbContext(config);

string dbName = db.Database.GetDbConnection().Database;
var res = db.Database.EnsureCreated();

if (res)
    WriteLine($"DB created {dbName}");