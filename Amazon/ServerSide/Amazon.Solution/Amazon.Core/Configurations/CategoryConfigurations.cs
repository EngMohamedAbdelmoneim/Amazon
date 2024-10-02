using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Amazon.Core.Entities;
namespace Amazon.Core.Configurations
{
    public class CategoryConfigurations
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(100);
            builder.Property(p => p.Type).IsRequired(false).HasMaxLength(100);
            builder.Property(p => p.DisplayOrder).IsRequired(false);
            builder.Property(p => p.IsActive).IsRequired(false);
            builder.Property(p=>p.IsVisible).IsRequired(false);

            builder.HasOne(p => p.ParentCategory)
                    .WithMany(c => c.Categories)
                    .HasForeignKey(p => p.ParentCategoryId);
        }
    }
}
