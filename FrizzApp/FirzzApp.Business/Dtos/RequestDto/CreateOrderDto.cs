using System.Collections.Generic;

namespace FirzzApp.Business.Dtos.RequestDto
{
    public class CreateOrderDto
    {
        public decimal PrecioTotal { get; set; }
        public bool EsDelivery { get; set; }
        public string DireccionDelivery { get; set; }
        public int MedioPagoId { get; set; }
        public List<int> ProductosId { get; set; }
    }
}
