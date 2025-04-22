using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace STS.Infrastructure.Cache.Redis;
public class RedisCacheServices(IDistributedCache distributedCache) : IRedisCacheServices
{
    public void Set<T>(string key, T value, int expireTime)
    {
        var cacheOption = new DistributedCacheEntryOptions()
        {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expireTime)
        };

        distributedCache.SetString(key,JsonSerializer.Serialize(value), cacheOption);
    }
    public void SetSliding<T>(string key, T value, int expireTime)
    {
        var cacheOption = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(expireTime)
        };

        distributedCache.SetString(key, JsonSerializer.Serialize(value), cacheOption);
    }
    public T Get<T>(string key)
    {
        var value = distributedCache.GetString(key);

        if (value != null)
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        return default;
    }
}