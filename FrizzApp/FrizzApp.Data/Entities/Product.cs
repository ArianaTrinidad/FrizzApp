using System;
using System.Collections.Generic;

namespace FrizzApp.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Presentation { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsDelete { get; set; }
        public string Notes { get; set; }
        //public bool IsVegan { get; set; }
        //public bool IsTacc { get; set; }

        public List<Tag> ProductByTag { get; set; }
    }



    public class ProductByTag
    {
        public int ProductByTagId { get; set; }
        public int TagId { get; set; }
        public int ProductId { get; set; }
    }



    public class Tag
    {
        public int TagId { get; set; }
    }



    public class Promo
    {
        public int PromoId { get; set; }
        public string Name { get; set; }
        public List<Product> Content { get; set; }
        public decimal Price { get; set; }
        public decimal PriceBefore { get; set; }
        public bool IsEnable { get; set; }
    }



    public class Order
    {
        public int OrderId { get; set; }
        public List<Product> Content { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public OrderState State { get; set; }
        public User User { get; set; }
        public bool HasDelivery { get; set; }
        public bool IsPaid { get; set; }
    }



    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsEnable { get; set; }
    }



    public class Address
    {
        public string AddressId { get; set; }
    }



    public enum PaymentType
    {
        Cash = 1,
        MercadoPago = 2,
        Debit = 3,
        Credit = 4,
    }


    public enum OrderState
    {
        Pending = 1,
        Done = 2,
        Canceled = 3,
    }



}
