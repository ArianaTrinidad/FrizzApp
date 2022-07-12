using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace FirzzApp.Business.Interfaces
{
    public interface ICacheService
    {
        TEntry Get<TEntry, TCacheDtoConfiguration>(string key, TCacheDtoConfiguration dtoConfig) where TCacheDtoConfiguration : ICacheable;
        void Remove(string key);
        bool Set<TValue>(string key, TValue dataToCache, TimeSpan? absoluteExpireTime = null);
    }
}
