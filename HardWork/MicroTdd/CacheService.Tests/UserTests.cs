using CacheService.Contracts;

namespace CacheService.Tests;

public class UserTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CreateUser_NullName_ThrowsArgumentNullException(string? name)
    {
        Assert.ThrowsAny<Exception>(() => User.CreateNew(name));
    }
    
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