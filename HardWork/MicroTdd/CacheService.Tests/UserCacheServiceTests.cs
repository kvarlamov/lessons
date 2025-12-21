using CacheService.Contracts;
using Xunit;

namespace CacheService.Tests;

public class UserCacheServiceTests
{
    private readonly UserCacheService _service;
    private readonly UserRepository _userRepository;

    public UserCacheServiceTests()
    {
        _userRepository = new UserRepository();
        _service = new UserCacheService(_userRepository);
    }
    
    [Fact]
    public void GetOrAdd_ReturnsValue()
    {
        var result = _service.GetOrAdd("test");
        Assert.Equal(default(User), result);
    }
}