using FrizzApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FrizzApp.Data.ConfigurationBuilders
{
    //public class OrderConfigurationBuilder : IEntityTypeConfiguration<Order>
    //{
    //    public void Configure(EntityTypeBuilder<Order> builder)
    //    {
    //        builder.HasKey(x => x.OrderId);

    //        builder.ToTable("Orders");

    //        builder.Property(b => b.TotalPrice)
    //               .IsRequired();

    //        builder.HasMany(c => c.Products)
    //            .WithOne(e => e.order);
    //    }
    //}
}
