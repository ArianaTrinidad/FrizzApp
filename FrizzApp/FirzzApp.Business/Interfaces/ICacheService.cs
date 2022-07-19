namespace FirzzApp.Business.Interfaces
{
    public interface ICacheService
    {
        TEntry Get<TEntry, TCacheDtoConfiguration>(string key, TCacheDtoConfiguration dtoConfig) where TCacheDtoConfiguration : ICacheable;
        bool Remove(string key);
        bool Set<TValue>(string key, TValue dataToCache); //DateTimeOffset expirationTime
    }
}
