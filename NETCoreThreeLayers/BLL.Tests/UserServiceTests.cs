using AutoMapper;
using BLL.Common;
using BLL.DTOs.Users;
using BLL.Services;
using DAL.Aggregates;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BLL.Tests
{
    public class UserServiceTests
    {
        private readonly IUserServices _userServices;
        private readonly Mock<ISharedRepositories> _sharedRepositories;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IConfiguration> _configuration;
        private readonly List<User> _users;

        public UserServiceTests()
        {
            _users = new List<User>()
            {
                new User { Username = "sample", Password = "sampass", Email = "sample@sample.sample", FullName = "Sample User", Id = new("3eaaabc4-746f-47f9-820a-54ad2c4660dd") },
                new User { Username = "sample2", Password = "sampass2", Email = "sample2@sample.sample", FullName = "Sample User Two", Id = new("5c87461c-8613-425f-89b3-83c7b741361e") },
                new User { Username = "sample3", Password = "sampass3", Email = "sample3@sample.sample", FullName = "Sample User Three", Id = new("270d8311-f96d-42b9-a62c-73e01c120d11") }
            };

            _configuration = new Mock<IConfiguration>();
            _sharedRepositories = new Mock<ISharedRepositories>();
            _mapper = new Mock<IMapper>();
            _userServices = new UserService(_sharedRepositories.Object, _mapper.Object, _configuration.Object);
        }

        [Fact]
        public void GetAllUser()
        {
            // Arrange
            int expectedCount = 3;
            string expectedUsername = "sample";
            _sharedRepositories.Setup(s => s.RepositoriesManager.UserRepository.GetAll()).Returns(_users);
            _mapper.Setup(s => s.Map<IEnumerable<UserDTO>>(_users)).Returns(new List<UserDTO>(){
                new UserDTO { Username = "sample",  Email = "sample@sample.sample",  Id = new("3eaaabc4-746f-47f9-820a-54ad2c4660dd") },
                new UserDTO { Username = "sample2", Email = "sample2@sample.sample", Id = new("5c87461c-8613-425f-89b3-83c7b741361e") },
                new UserDTO { Username = "sample3",  Email = "sample3@sample.sample",  Id = new("270d8311-f96d-42b9-a62c-73e01c120d11") }
            });

            // Act
            IEnumerable<UserDTO> actual = _userServices.GetAllUser();
            int actualCount = actual.Count();
            UserDTO actualUser = actual.First();

            // Assert
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(expectedUsername, actualUser.Username);
        }

        [Fact]
        public void InsertUser()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedNewUsername = "sample4";
            string password = "samplass4";
            var newUser = new User
            {
                Id = expectedId,
                Username = expectedNewUsername,
            };
            var registerRequest = new RegisterRequest
            {
                Username = expectedNewUsername,
                Password = password
            };
            _mapper.Setup(s => s.Map<User>(registerRequest)).Returns(newUser);
            _sharedRepositories.Setup(s => s.RepositoriesManager.UserRepository.Insert(newUser)).Callback(() => _users.Add(newUser)).Returns(newUser);
            // Act

            Guid actualId = _userServices.Register(registerRequest);
            // Assert

            Assert.Equal(expectedId, actualId);
        }
    }
}