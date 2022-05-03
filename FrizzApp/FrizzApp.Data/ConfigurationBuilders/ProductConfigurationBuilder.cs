using FrizzApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrizzApp.Data.ConfigurationBuilders
{
    public class ProductConfigurationBuilder : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Products");

            builder.Property(b => b.Name)
                   .IsRequired(true)
                   .HasMaxLength(50);

            builder.Property(b => b.Description)
                   .IsRequired(false)
                   .HasMaxLength(120);

            builder.Property(b => b.Notes)
                   .IsRequired(false)
                   .HasMaxLength(150);

            builder.Property(b => b.Presentation)
                   .IsRequired(false)
                   .HasMaxLength(20);

            builder.Property(b => b.ImageUrl)
                   .IsRequired(false);

            builder.Property(b => b.Price)
                   .IsRequired()
                   .HasPrecision(7, 2)
                   .HasDefaultValue(0);

            builder.Property(b => b.OldPrice)
                   .IsRequired(false)
                   .HasPrecision(7, 2);

            builder.Property(b => b.IsPromo)
                   .HasDefaultValue(false);
        }
    }
}
