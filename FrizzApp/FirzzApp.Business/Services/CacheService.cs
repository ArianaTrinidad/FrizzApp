﻿using FirzzApp.Business.Enums;
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

            var connectionString = _configuration.GetValue<string>("CacheConfiguration:ConnectionStrings:Redis");
            var redis = ConnectionMultiplexer.Connect(connectionString);
            _db = redis.GetDatabase();
        }

        public TEntry Get<TEntry, TCacheDtoConfiguration>(string key, TCacheDtoConfiguration dtoConfig)
            where TCacheDtoConfiguration : ICacheable
        {
            var value = _db.StringGet(key);

            var cacheConfigStatus = _configuration.GetValue<bool>("CacheConfiguration:UseCache");

            if (dtoConfig.CacheType == CacheTypeEnum.UseCache && cacheConfigStatus == true && value.HasValue)
            {
                return JsonConvert.DeserializeObject<TEntry>(value);
            }

            return default;
        }


        public bool Set<TValue>(string key, TValue dataToCache)
        {
            var defaultExpiration = _configuration.GetValue<int>("CacheConfiguration:DefaultExpirationInSeconds");
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
