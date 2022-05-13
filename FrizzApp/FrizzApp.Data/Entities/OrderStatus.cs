namespace FrizzApp.Data.Entities
{
    public class OrderStatus
    {
        private string name;

        public OrderStatusEnum OrderStatusId { get; set; }

        public string StatusName
        {
            get { return OrderStatusId.ToString(); }
            set { name = OrderStatusId.ToString(); }
        }
    }

    public enum OrderStatusEnum
    {
        Pending = 1,
        Done = 2,
        Canceled = 3,
    }
}
