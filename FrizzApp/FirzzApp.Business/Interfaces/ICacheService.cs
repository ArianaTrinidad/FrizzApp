using Microsoft.Extensions.Caching.Memory;

namespace FirzzApp.Business.Interfaces
{
    public interface ICacheService
    {
        TEntry Get<TEntry, TCacheDtoConfiguration>(string key, TCacheDtoConfiguration dtoConfig) where TCacheDtoConfiguration : ICacheable;
        void Remove(string key);
        void Set<TValue>(string key, TValue dataToCache, MemoryCacheEntryOptions options = null);
    }
}
