using FirzzApp.Business.Enums;
using FirzzApp.Business.Interfaces;

namespace FirzzApp.Business.Dtos.RequestDto
{
    public class GetAllProductDto : ICacheable
    {
        public string Busqueda { get; set; }
        public int NumeroPagina { get; set; }
        public int CantidadPagina { get; set; }
        public int? CategoriaId { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public CacheTypeEnum CacheType { get; set; }
    }
}
