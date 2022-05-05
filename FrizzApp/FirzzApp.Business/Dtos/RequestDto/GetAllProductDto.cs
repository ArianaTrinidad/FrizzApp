namespace FirzzApp.Business.Dtos.RequestDto
{
    public class GetAllProductDto
    {
        public string Busqueda { get; set; }
        public int NumeroPagina { get; set; }
        public int CantidadPagina { get; set; }
        public int? CategoriaId { get; set; }
        public int? PrecioMinimo { get; set; }
        public int? PrecioMaximo { get; set; }
    }
}
