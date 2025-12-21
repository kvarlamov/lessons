namespace CacheService;

public class CacheService<T> : ICacheService<T>
    where T : class
{
    public T GetOrAdd(string key, T defaultValue)
    {
        return default;
    }
}