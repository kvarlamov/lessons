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
        if (!_cache.TryGetValue(key, out var user))
        {
            return null;
        }
        
        return user;
    }
}

public sealed class UserRepository
{
    
} 