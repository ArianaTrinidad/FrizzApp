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
        public int? PrecioMinimo { get; set; }
        public int? PrecioMaximo { get; set; }
        public CacheTypeEnum CacheType { get; set; }
    }
}
