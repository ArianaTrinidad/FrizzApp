using FirzzApp.Business.Enums;

namespace FirzzApp.Business.Interfaces
{
    public interface ICacheable
    {
        CacheTypeEnum CacheType { get; set; }
    }
}
