using CacheService.Contracts;

namespace CacheService;

public class UserCacheService : ICacheService<User>
{
    private readonly UserRepository _repository;
    private readonly Dictionary<string, User> _cache;

    public UserCacheService(UserRepository repository)
    {
        _repository = repository;
        _cache = new Dictionary<string, User>();
    }
    
    public User GetOrAdd(string key)
    {
        if (!long.TryParse(key, out var userId))
        {
            throw new ArgumentException($"key should be long: {key}");
        }

        if (_cache.TryGetValue(key, out var user)) return user;
        
        var existingUserResult = _repository.GetUser(userId);
        if (!existingUserResult.IsSuccess)
        {
            throw new InvalidOperationException($"User with key {key} was not found");
        }
        _cache.Add(key, existingUserResult.Value);
        return existingUserResult.Value;

    }
}