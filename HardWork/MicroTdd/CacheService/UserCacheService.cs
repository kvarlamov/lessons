using CacheService.Contracts;

namespace CacheService;

public class UserCacheService : ICacheService<User>
{
    private readonly Dictionary<string, User> _cache;

    public UserCacheService()
    {
        _cache = new Dictionary<string, User>();
    }
    
    public User GetOrAdd(string key)
    {
        return default;
    }
}