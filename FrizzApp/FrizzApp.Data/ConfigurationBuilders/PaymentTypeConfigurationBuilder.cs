using FrizzApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrizzApp.Data.ConfigurationBuilders
{
    public class PaymentTypeConfigurationBuilder : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(x => x.PaymentTypeId);

            builder.ToTable("PaymentTypes");

            builder.Property(b => b.PaymentTypeName)
                   .IsRequired(true)
                   .HasMaxLength(20);
        }
    }
}
