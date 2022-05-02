using FrizzApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrizzApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }



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
