using FirzzApp.Business.Enums;
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

        public TEntry Get<TEntry, TCacheDtoConfiguration>(string key, TCacheDtoConfiguration dtoConfig)
            where TCacheDtoConfiguration : ICacheable
        {
            TEntry entry = default;

            var cacheConfigStatus = _configuration.GetValue<bool>("CacheConfiguration:UseCache");

            if (dtoConfig.CacheType == CacheTypeEnum.UseCache && cacheConfigStatus == true)
            {
                _cache.TryGetValue(key, out entry);
            }

            return entry;
        }


        public void Set<TValue>(string key, TValue dataToCache, MemoryCacheEntryOptions options = null)
        {
            var cacheConfigLifeTime = _configuration.GetValue<int>("CacheConfiguration:DefaultLifeTime");
            var cacheConfigSize = _configuration.GetValue<int>("CacheConfiguration:DefaultSize");

            var defaultOptions = new MemoryCacheEntryOptions()
            {
                Size = cacheConfigSize,
                SlidingExpiration = TimeSpan.FromSeconds(cacheConfigLifeTime)
            };

            _cache.Set(key, dataToCache, options ?? defaultOptions);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

    }
}
