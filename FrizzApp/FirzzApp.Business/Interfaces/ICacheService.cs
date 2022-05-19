using Microsoft.Extensions.Caching.Memory;

namespace FirzzApp.Business.Interfaces
{
    public interface ICacheService
    {
        TEntry Get<TEntry>(string key);
        void Remove(string key);
        void Set<TValue>(string key, TValue dataToCache, MemoryCacheEntryOptions options = null);
    }
}
