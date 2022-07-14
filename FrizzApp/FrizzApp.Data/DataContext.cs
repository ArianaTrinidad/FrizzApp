using DocumentFormat.OpenXml.InkML;
using FrizzApp.Data.ConfigurationBuilders;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Enums;
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
            modelBuilder.ApplyConfiguration(new OrderStatusConfigurationBuilder());
            modelBuilder.ApplyConfiguration(new PaymentTypeConfigurationBuilder());
            modelBuilder.ApplyConfiguration(new ProductStatusConfigurationBuilder());


            modelBuilder.Entity<Product>()
                .HasQueryFilter(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted);
        }



        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<OrderStatus> OrderStates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }




        private static void SeedInitialData(ModelBuilder modelBuilder)
        {
            var productsStatusInitialData = new List<ProductStatus>()
            {
                new ProductStatus(){ ProductStatusId = (int)ProductStatusEnum.Avaiable, Name = nameof(ProductStatusEnum.Avaiable)},
                new ProductStatus(){ ProductStatusId = (int)ProductStatusEnum.WithoutStock, Name = nameof(ProductStatusEnum.WithoutStock)},
                new ProductStatus(){ ProductStatusId = (int)ProductStatusEnum.Deleted, Name = nameof(ProductStatusEnum.Deleted)},
            };

            var orderStatusInitialData = new List<OrderStatus>()
            {
                new OrderStatus(){ OrderStatusId = (int)OrderStatusEnum.Pending, StatusName = nameof(OrderStatusEnum.Pending)},
                new OrderStatus(){ OrderStatusId = (int)OrderStatusEnum.Done, StatusName = nameof(OrderStatusEnum.Done)},
                new OrderStatus(){ OrderStatusId = (int)OrderStatusEnum.Canceled, StatusName = nameof(OrderStatusEnum.Canceled)},
            };

            var paymentTypesInitialData = new List<PaymentType>()
            {
                new PaymentType(){ PaymentTypeId = (int)PaymentTypeEnum.Cash, PaymentTypeName= nameof(PaymentTypeEnum.Cash)},
                new PaymentType(){ PaymentTypeId = (int)PaymentTypeEnum.MercadoPago, PaymentTypeName= nameof(PaymentTypeEnum.MercadoPago)},
                new PaymentType(){ PaymentTypeId = (int)PaymentTypeEnum.Debit, PaymentTypeName= nameof(PaymentTypeEnum.Debit)},
                new PaymentType(){ PaymentTypeId = (int)PaymentTypeEnum.Credit, PaymentTypeName= nameof(PaymentTypeEnum.Credit)}
            };

            modelBuilder.Entity<ProductStatus>().HasData(productsStatusInitialData);
            modelBuilder.Entity<OrderStatus>().HasData(orderStatusInitialData);
            modelBuilder.Entity<PaymentType>().HasData(paymentTypesInitialData);
        }
    }
}
