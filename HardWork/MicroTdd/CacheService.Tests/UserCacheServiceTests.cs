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
    public void GetOrAdd_KeyIsntCorrectFormat_Throw()
    {
        Assert.ThrowsAny<Exception>(() => _service.GetOrAdd("test"));
    }

    [Fact]
    public void GetOrAdd_UserNotExistInDb_Throw()
    {
        Assert.ThrowsAny<Exception>(() => _service.GetOrAdd("1"));
    }

    [Fact]
    public void GetOrAdd_UserExistInDb_ReturnsExistingUser()
    {
        var existingUser = _userRepository.AddNew(User.CreateNew("test"));
        
        var cacheResult = _service.GetOrAdd(existingUser.Id.ToString());
        Assert.Equal(existingUser, cacheResult);
    }
}