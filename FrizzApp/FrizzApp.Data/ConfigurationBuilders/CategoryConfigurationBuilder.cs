using FrizzApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrizzApp.Data.ConfigurationBuilders
{
    public class CategoryConfigurationBuilder : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.CategoryId);

            builder.ToTable("Categories");

            builder.Property(b => b.CategoryName)
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}
