namespace FirzzApp.Business.Dtos.RequestDto
{
    public class GetAllProductDto
    {
        public string Busqueda { get; set; }
        public int? Categoria { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public int NumeroPagina { get; set; }
        public int CantidadPagina { get; set; }
    }
}
