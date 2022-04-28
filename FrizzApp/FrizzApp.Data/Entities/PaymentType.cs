namespace FrizzApp.Data.Entities
{
    public class PaymentType
    {
        private string name;

        public PaymentTypeEnum PaymentTypeId { get; set; }

        public string PaymentTypeName
        {
            get { return PaymentTypeId.ToString(); }
            set { name = PaymentTypeId.ToString(); }
        }
    }

    public enum PaymentTypeEnum
    {
        Cash = 1,
        MercadoPago = 2,
        Debit = 3,
        Credit = 4,
    }

}
