using CacheService.Contracts;

namespace CacheService;

public sealed class UserRepository
{
    private static int _counter;
    private readonly List<User> _repo;

    public UserRepository()
    {
        _repo = new List<User>();
    }
    
    public Result<User> GetUser(long id)
    {
        var result = _repo.FirstOrDefault(x => x.Id == id);
        
        return result == null 
            ? Result<User>.Failure("User not found") 
            : Result<User>.Success(result);
    }

    public User AddNew(User user)
    {
        var newUser = User.Create(++_counter, user.FirstName);
        _repo.Add(newUser);
        
        return _repo.First(x => x.Id == _counter);
    }
}