using System;
using System.Collections.Generic;

namespace FrizzApp.Data.Entities
{
    public class Order : AuditableEntity
    {
        public Order()
        {
            Products = new List<Product>();
        }

        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDelivery { get; set; }
        public string DeliveryAddress { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public bool IsPaid { get; set; }

        public int PaymentTypeId { get; set; }
        public int OrderStatusId { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
