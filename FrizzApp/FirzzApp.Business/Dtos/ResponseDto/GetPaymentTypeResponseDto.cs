using FrizzApp.Data.Entities;

namespace FirzzApp.Business.Dtos.ResponseDto
{
    public class GetPaymentTypeResponseDto
    {
        public PaymentTypeEnum Id { get; set; }

        public string Nombre { get; set; }
    }
}
