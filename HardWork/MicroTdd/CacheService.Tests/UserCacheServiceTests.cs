using CacheService.Contracts;
using Xunit;

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
        var result = _service.GetOrAdd("test");
        Assert.Equal(default(User), result);
    }
}