using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrizzApp.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDelivery { get; set; }
        public string DeliveryAddress { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public bool IsPaid { get; set; }

        public PaymentTypeEnum PaymentTypeId { get; set; }
        public OrderStatusEnum OrderStatusId { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }


        // public int UserId { get; set; }
        //public virtual List<Product> Products { get; set; } Línea de la discordía...
    }
}
