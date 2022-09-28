namespace DAL.Tests;

using DAL.Aggregates;
using DAL.Configs;
using DAL.Repositories;
using Xunit;

public class UserRepositoryTest
{
    private readonly IGenericRepository<User> _repository;
    private readonly RepositoryContext _context;
    private readonly IConfig _config;

    public UserRepositoryTest()
    {
        _config = new InMemoryDBConfig();
        _context = new RepositoryContext(_config);
        _context.Database.EnsureCreated();
        _repository = new GenericRepository<User>(_context);
    }

    [Fact]
    public void TestGetAll()
    {
        // Arrange
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        var expectedUserCount = 3;
        // Act
        var actualUserCount = _repository.GetAll().Count();
        // Assert
        Assert.Equal(expectedUserCount, actualUserCount);
    }

    [Fact]
    public void TestGetSingle()
    {
        // Arrange
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        var expectedUser = new User { Username = "sample", Password = "sampass", Email = "sample@sample.sample", FullName = "Sample User", Id = new("3eaaabc4-746f-47f9-820a-54ad2c4660dd") };

        // Act
        var actualUser = _repository.GetById(new Guid("3eaaabc4-746f-47f9-820a-54ad2c4660dd"));
        // Assert
        if (actualUser == null)
        {
            Assert.True(false);
            return;
        }

        Assert.Equal(expectedUser.Id, actualUser.Id);
        Assert.Equal(expectedUser.Username, actualUser.Username);
    }

    [Fact]
    public void TestGetSingleReturnNull()
    {
        // Arrange
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        // Act
        var actualUser = _repository.GetById(Guid.Empty);
        // Assert
        Assert.Null(actualUser);
    }

    [Fact]
    public void TestInsert()
    {
        // Arrange
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        var newGuid = Guid.NewGuid();
        var newUser = new User
        {
            Id = newGuid,
            Username = "test",
            Email = "test@test",
            Password = "testpass",
            FullName = "Test Name"
        };
        // Act
        _repository.Insert(newUser);
        _context.SaveChanges();
        var actualUser = _repository.GetById(newGuid);

        // Assert
        Assert.Equal(newUser.Username, actualUser.Username);
    }

    [Fact]
    public void TestUpdate()
    {
        // Arrange
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        var updateID = new Guid("270d8311-f96d-42b9-a62c-73e01c120d11");
        var updateUsename = "Updated";
        var updatedUser = new User
        {
            Id = updateID,
            Username = updateUsename,
        };

        // Act
        _repository.Update(updatedUser);
        _context.SaveChanges();
        var actualUser = _repository.GetById(updateID);

        // Assert

        Assert.Equal(updateUsename, actualUser.Username);
    }

    [Fact]
    public void TestDelete()
    {
        // Arrange
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        var deleteId = new Guid("5c87461c-8613-425f-89b3-83c7b741361e");
        // Act
        _repository.Delete(deleteId);
        _context.SaveChanges();
        var actualUser = _repository.GetById(deleteId);
        // Assert
        Assert.Null(actualUser);
    }
}