using FluentAssertions;

using Metro60.Core.Data;
using Metro60.Core.Entities;
using Metro60.Core.Services;

using Microsoft.EntityFrameworkCore;

namespace Metro60.UnitTests.Services;

[TestFixture]
[Category("UserService")]
public class UserServiceTests
{
    private TestScope _scope;
    private List<User> _users;

    [SetUp]
    public void Setup()
    {
        _users = new List<User>
        {
            new()
            {
                Id = 100,
                FirstName = "First Name 1",
                LastName = "Last Name 1",
                Username = "User 1",
                Password = "Password"
            },
            new()
            {
                Id = 200,
                FirstName = "First Name 2",
                LastName = "Last Name 2",
                Username = "User 2",
                Password = "Password"
            }
        };
        _scope = new TestScope();
        _scope.Context.Users.AddRange(_users);
        _scope.Context.SaveChanges();
    }

    [Test]
    public async Task GetAll_ReturnsAllUsers_WhenCalled()
    {
        //act
        var allUsers = await _scope.ServiceUnderTest.GetAll();

        //assert
        allUsers.Count.Should().Be(_users.Count);

        foreach (var userModel in allUsers)
        {
            var expectedUser = _users.SingleOrDefault(x => x.Id == userModel.Id);

            expectedUser.Should().NotBeNull();
            expectedUser.Username.Should().Be(userModel.Username);
            expectedUser.FirstName.Should().Be(userModel.FirstName);
            expectedUser.LastName.Should().Be(userModel.LastName);
        }
    }

    [TestCase("User 1", "Password", 100)]
    [TestCase("User 2", "Password", 200)]
    public async Task GetUser_ReturnsSpecifiedUsers_WhenCalled(string username, string password, int expectedId)
    {
        //act
        var user = await _scope.ServiceUnderTest.GetUser(username, password);

        //assert
        user.Should().NotBeNull();
        user.Id.Should().Be(expectedId);
    }

    private class TestScope
    {
        internal MetroDbContext Context { get; set; }
        internal UserService ServiceUnderTest { get; init; }

        public TestScope()
        {
            var dbContext = new MetroDbContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: $"TestDatabase{Guid.NewGuid()}")
                .Options);

            Context = dbContext;
            ServiceUnderTest = new UserService(dbContext);

        }
    }
}
