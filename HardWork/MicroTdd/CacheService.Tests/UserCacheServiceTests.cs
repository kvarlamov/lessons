using CacheService.Contracts;

namespace CacheService.Tests;

public class UserCacheServiceTests
{
    private readonly CacheService<User> _service;

    public UserCacheServiceTests()
    {
        _service = new CacheService<User>();
    }
    
    [Fact]
    public void GetOrAdd_ReturnsValue()
    {
    }
}

public class UserTests
{
    [Fact]
    public void CreateNew()
    {
        // Act
        var user = User.CreateNew("test");
        
        // Assert
        Assert.NotNull(user);
        Assert.Equal("test", user.FirstName);
        Assert.Equal(0, user.Id);
    }
}