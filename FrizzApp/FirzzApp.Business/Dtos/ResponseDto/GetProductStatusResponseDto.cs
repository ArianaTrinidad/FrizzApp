using FrizzApp.Data.Entities;


namespace FirzzApp.Business.Dtos.ResponseDto
{
    public class GetProductStatusResponseDto
    {
        public ProductStatusEnum Id { get; set; }

        public string Nombre;
    }
}
