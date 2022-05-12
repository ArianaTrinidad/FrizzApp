using FrizzApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrizzApp.Data.ConfigurationBuilders
{
    public class ProductStatusConfigurationBuilder : IEntityTypeConfiguration<ProductStatus>
    {
        public void Configure(EntityTypeBuilder<ProductStatus> builder)
        {
            builder.HasKey(x => x.ProductStatusId);

            builder.ToTable("ProductStatus");

            builder.Property(b => b.Name)
                   .IsRequired(true)
                   .HasMaxLength(20);
        }
    }
}
