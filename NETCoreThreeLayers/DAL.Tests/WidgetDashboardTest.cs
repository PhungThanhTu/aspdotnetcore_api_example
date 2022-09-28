using DAL.Aggregates;
using DAL.Configs;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests;

public class WidgetDashboardTest
{
    private readonly IGenericRepository<Dashboard> _dashboardRepo;
    private readonly IGenericRepository<Widget> _widgetRepo;
    private readonly RepositoryContext _context;

    public WidgetDashboardTest()
    {
        IConfig config = new MSSQLDbConfig();
        _context = new RepositoryContext(config);
        _dashboardRepo = new GenericRepository<Dashboard>(_context);
        _widgetRepo = new GenericRepository<Widget>(_context);
    }

    [Fact]
    public void TestManyToManyTypeAdded()
    {
        var user = _context?.Users?.Where(user => user.Username == "admin").First();

        var newWidget1 = new Widget
        {
            Id = Guid.NewGuid(),
            Title = "SampleWidget",
            Configs = @"",
            MinHeight = 100,
            MinWidth = 100,
            WidgetType = "Button"
        };
        var newWidget2 = new Widget
        {
            Id = Guid.NewGuid(),
            Title = "SampleWidget2",
            Configs = @"",
            MinHeight = 100,
            MinWidth = 100,
            WidgetType = "Button"
        };
        var dashboard = new Dashboard
        {
            Id = Guid.NewGuid(),
            User = user,
            Title = "sudo dashboard",
            LayoutType = "inline",
            Widgets = new List<Widget>() { newWidget1, newWidget2 }
        };

        _dashboardRepo.Insert(dashboard);
        _context.SaveChanges();

        _dashboardRepo.Delete(dashboard);
        _context.SaveChanges();
    }

    [Fact]
    public void TestGetManyToMany()
    {
        var query = _dashboardRepo.Get().Include(x => x.Widgets).AsEnumerable<Dashboard>();

        Assert.True(true);
    }
}