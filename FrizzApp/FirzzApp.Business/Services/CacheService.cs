using FirzzApp.Business.Enums;
using FirzzApp.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace FirzzApp.Business.Services
{
    public class CacheService : ICacheService
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabase _db;


        public CacheService(IConfiguration configuration)
        {
            _configuration = configuration;

            var redisConfig = new ConfigurationOptions
            {
                EndPoints = { _configuration.GetValue<string>("RedisConfiguration:Url")},
                Ssl = true,
                AllowAdmin = true,
                Password = _configuration.GetValue<string>("RedisConfiguration:Password")
            };

            var redis = ConnectionMultiplexer.Connect(redisConfig);
            _db = redis.GetDatabase();
        }

        public TEntry Get<TEntry, TCacheDtoConfiguration>(string key, TCacheDtoConfiguration dtoConfig)
            where TCacheDtoConfiguration : ICacheable
        {
            var value = _db.StringGet(key);

            var cacheConfigStatus = _configuration.GetValue<bool>("RedisConfiguration:UseCache");

            if (dtoConfig.CacheType == CacheTypeEnum.UseCache && cacheConfigStatus == true && value.HasValue)
            {
                return JsonConvert.DeserializeObject<TEntry>(value);
            }

            return default;
        }


        public bool Set<TValue>(string key, TValue dataToCache)
        {
            var defaultExpiration = _configuration.GetValue<int>("RedisConfiguration:DefaultExpirationInSeconds");
            return _db.StringSet(key, JsonConvert.SerializeObject(dataToCache), TimeSpan.FromSeconds(defaultExpiration));
        }

        public bool Remove(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;

        }
    }
}
