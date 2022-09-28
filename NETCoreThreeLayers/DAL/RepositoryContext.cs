using DAL.Aggregates;
using DAL.Configs;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class RepositoryContext : DbContext
{
    private readonly IConfig _config;

    public DbSet<User>? Users { get; set; }
    public DbSet<TaskModel>? Tasks { get; set; }

    public DbSet<Contact>? Contacts { get; set; }

    public DbSet<Widget>? Widgets { get; set; }

    public DbSet<Dashboard>? Dashboard { get; set; }

    public DbSet<DashboardWidget>? DashboardWidgets { get; set; }

    public RepositoryContext(IConfig config)
    {
        _config = config;
    }

    public RepositoryContext(DbContextOptions<RepositoryContext> options, IConfig config) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        _config.UseDBMS(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _config.OnModelCreating(modelBuilder);
    }
}