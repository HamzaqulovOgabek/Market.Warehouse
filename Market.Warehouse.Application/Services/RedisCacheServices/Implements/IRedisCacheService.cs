
namespace Market.Warehouse.Application.Services.RedisCacheServices
{
    public interface IRedisCacheService
    {
        Task<T> GetCacheAsync<T>(string key);
        Task RemoveCacheAsync(string key);
        Task SetCacheAsync<T>(string key, T data, TimeSpan expiration);
    }
}