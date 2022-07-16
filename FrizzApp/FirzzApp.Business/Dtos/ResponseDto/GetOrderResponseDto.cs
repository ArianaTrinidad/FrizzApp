namespace FirzzApp.Business.Dtos.ResponseDto
{
    public class GetOrderResponseDto
    {
        public int OdernId { get; set; }
        public string Fecha { get; set; }
        public decimal PrecioTotal { get; set; }
        public bool EsDelivery { get; set; }
        public bool EstaPago { get; set; }
        public int CantidadProductos { get; set; }
    }
}
