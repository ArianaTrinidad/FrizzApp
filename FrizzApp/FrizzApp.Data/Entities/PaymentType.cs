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
        Cash = 10,
        MercadoPago = 11,
        Debit = 13,
        Credit = 14,
    }

}
