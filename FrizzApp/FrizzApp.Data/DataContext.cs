using FrizzApp.Data.ConfigurationBuilders;
using FrizzApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FrizzApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedInitialData(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfigurationBuilder());
            modelBuilder.ApplyConfiguration(new ProductConfigurationBuilder());
        }



        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Category> Categories { get; set; }


        private static void SeedInitialData(ModelBuilder modelBuilder)
        {
            var productsStatusInitialData = new List<ProductStatus>()
            {
                new ProductStatus(){ ProductStatusId = ProductStatusEnum.Avaiable, Name = nameof(ProductStatusEnum.Avaiable)},
                new ProductStatus(){ ProductStatusId = ProductStatusEnum.WithoutStock, Name = nameof(ProductStatusEnum.WithoutStock)},
                new ProductStatus(){ ProductStatusId = ProductStatusEnum.Deleted, Name = nameof(ProductStatusEnum.Deleted)},
            };

            var orderStatusInitialData = new List<OrderStatus>()
            {
                new OrderStatus(){ OrderStatusId = OrderStatusEnum.Pending, StatusName = nameof(OrderStatusEnum.Pending)},
                new OrderStatus(){ OrderStatusId = OrderStatusEnum.Done, StatusName = nameof(OrderStatusEnum.Done)},
                new OrderStatus(){ OrderStatusId = OrderStatusEnum.Canceled, StatusName = nameof(OrderStatusEnum.Canceled)},
            };

            var paymentTypesInitialData = new List<PaymentType>()
            {
                new PaymentType(){ PaymentTypeId = PaymentTypeEnum.Cash, PaymentTypeName= nameof(PaymentTypeEnum.Cash)},
                new PaymentType(){ PaymentTypeId = PaymentTypeEnum.MercadoPago, PaymentTypeName= nameof(PaymentTypeEnum.MercadoPago)},
                new PaymentType(){ PaymentTypeId = PaymentTypeEnum.Debit, PaymentTypeName= nameof(PaymentTypeEnum.Debit)},
                new PaymentType(){ PaymentTypeId = PaymentTypeEnum.Credit, PaymentTypeName= nameof(PaymentTypeEnum.Credit)}
            };

            var categoriesInitialData = new List<Category>()
            {
                new Category(){ CategoryId = 100, CategoryName= "Panaderia"},
                new Category(){ CategoryId = 101, CategoryName= "Salado"},
                new Category(){ CategoryId = 102, CategoryName= "Dulce"}
            };

            modelBuilder.Entity<ProductStatus>().HasData(productsStatusInitialData);
            modelBuilder.Entity<OrderStatus>().HasData(orderStatusInitialData);
            modelBuilder.Entity<PaymentType>().HasData(paymentTypesInitialData);
            modelBuilder.Entity<Category>().HasData(categoriesInitialData);
        }
    }
}
