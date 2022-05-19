using FirzzApp.Business.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;

namespace FirzzApp.Business.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public CacheService(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public TEntry Get<TEntry>(string key)
        {
            TEntry entry = default;

            _cache.TryGetValue(key, out entry);

            return entry;
        }


        public void Set<TValue>(string key, TValue dataToCache, MemoryCacheEntryOptions options = null)
        {
            var defaultOptions = new MemoryCacheEntryOptions()
            {
                Size = 10000,
                SlidingExpiration = TimeSpan.FromSeconds(1000)
            };

            _cache.Set(key, dataToCache, options ?? defaultOptions);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

    }
}
