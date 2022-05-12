using FrizzApp.Data.Entities;


namespace FirzzApp.Business.Dtos.RequestDto
{
    public class DeletePaymentTypeDto
    {
        public PaymentTypeEnum Id { get; set; }

        //No me parece necesario public string Nombre; 
    }
}
