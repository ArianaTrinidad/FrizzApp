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
            var productsStatus = new List<ProductStatus>()
            {
                new ProductStatus(){ ProductStatusId = ProductStatusEnum.Avaiable, Name = nameof(ProductStatusEnum.Avaiable)},
                new ProductStatus(){ ProductStatusId = ProductStatusEnum.WithoutStock, Name = nameof(ProductStatusEnum.WithoutStock)},
                new ProductStatus(){ ProductStatusId = ProductStatusEnum.Deleted, Name = nameof(ProductStatusEnum.Deleted)},
            };
            modelBuilder.Entity<ProductStatus>().HasData(productsStatus);
            
            
            modelBuilder.ApplyConfiguration(new CategoryConfigurationBuilder());
            modelBuilder.ApplyConfiguration(new ProductConfigurationBuilder());
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Category> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderStatus> OrderStates { get; set; }

        //public DbSet<ProductByOrder> ProductsByOrder { get; set; }

        //public DbSet<User> Users { get; set; }
        //public DbSet<ProductByTag> ProductsByTag { get; set; }
        //public DbSet<Promo> Promos { get; set; }
    }
}
