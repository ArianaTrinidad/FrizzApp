using FrizzApp.Data.Entities;

namespace FirzzApp.Business.Dtos.RequestDto
{
    public class DeleteProductStatusDto
    {
        public ProductStatusEnum Id { get; set; }

       //No me parece necesario public string Nombre;
    }
}
