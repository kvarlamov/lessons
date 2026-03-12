using CacheService.Contracts;

namespace CacheService.Tests;

public class UserRepositoryTests
{
    private readonly UserRepository _repo;

    public UserRepositoryTests()
    {
        _repo = new UserRepository();
    }

    [Fact]
    public void GetUser_NotExist_ReturnFailure()
    {
        var userResult = _repo.GetUser(1);
        Assert.False(userResult.IsSuccess);
        Assert.Null(userResult.Value);
    }

    [Fact]
    public void AddUser_IdIsCorrect()
    {
        var savedUser = _repo.AddNew(User.Create(5, "test"));
        Assert.Equal(1, savedUser.Id);
    }
    
    [Fact]
    public void GetUser_Exist_ReturnSuccess()
    {
        // Arrange
        _repo.AddNew(User.CreateNew("test"));
        
        //Act
        var userResult = _repo.GetUser(1);
        
        //Assert
        Assert.True(userResult.IsSuccess);
        Assert.NotNull(userResult.Value);
        Assert.Equal("test", userResult.Value.FirstName);
    }
}