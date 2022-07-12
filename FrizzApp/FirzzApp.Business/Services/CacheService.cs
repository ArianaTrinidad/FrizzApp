using FirzzApp.Business.Enums;
using FirzzApp.Business.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Net.Http;

namespace FirzzApp.Business.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly IDatabase _db;

        //public CacheService(IDistributedCache cache, IConfiguration configuration)
        //{
        //    _cache = cache;
        //    _configuration = configuration;
        //}

        public CacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            
            var connectionString = _configuration.GetValue<string>("CacheConfiguration:ConnectionStrings:Redis");
            var redis = ConnectionMultiplexer.Connect(connectionString);
            _db = redis.GetDatabase();
        }

        public TEntry Get<TEntry, TCacheDtoConfiguration>(string key, TCacheDtoConfiguration dtoConfig)
            where TCacheDtoConfiguration : ICacheable
        {
            //TEntry entry = default;
            var value = _db.StringGet(key);

            var cacheConfigStatus = _configuration.GetValue<bool>("CacheConfiguration:UseCache");

            if (dtoConfig.CacheType == CacheTypeEnum.UseCache && cacheConfigStatus == true)
            {
                //_cache.TryGetValue(key, out entry);
                return JsonConvert.DeserializeObject<TEntry>(value);
            }

            return default;
        }


        public bool Set<TValue>(string key, TValue dataToCache, TimeSpan? absoluteExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(600);

            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(dataToCache));
            return isSet;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

    }
}
