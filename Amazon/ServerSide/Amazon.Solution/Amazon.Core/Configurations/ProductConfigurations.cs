using Amazon.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amazon.Core.Configurations
{
    public class ProductConfigurations
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(p => p.ProductBrand)
                   .WithMany()
                   .HasForeignKey(p => p.ProductBrandId);

            builder.HasOne(p => p.ProductCategory)
                   .WithMany()
                   .HasForeignKey(p => p.ProductCategoryId);

        }
    }
}
