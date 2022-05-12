using FrizzApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrizzApp.Data.ConfigurationBuilders
{
    public class OrderStatusConfigurationBuilder : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(x => x.OrderStatusId);

            builder.ToTable("ProductStates");

            builder.Property(b => b.StatusName)
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}