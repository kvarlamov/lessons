namespace CacheService;

public interface ICacheService<T>
{
    T GetOrAdd(string key, T defaultValue);
}