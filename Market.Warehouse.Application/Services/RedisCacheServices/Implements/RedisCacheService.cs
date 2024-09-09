using Newtonsoft.Json;
using StackExchange.Redis;

namespace Market.Warehouse.Application.Services.RedisCacheServices;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<T> GetCacheAsync<T>(string key)
    {
        var cachedData = await _database.StringGetAsync(key);
        if (!cachedData.IsNullOrEmpty)
        {
            return JsonConvert.DeserializeObject<T>(cachedData);
        }

        return default(T);
    }
    public async Task SetCacheAsync<T>(string key, T data, TimeSpan expiration)
    {
        var jsonData = JsonConvert.SerializeObject(data);
        await _database.StringSetAsync(key, jsonData, expiration);
    }
    public async Task RemoveCacheAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

}
