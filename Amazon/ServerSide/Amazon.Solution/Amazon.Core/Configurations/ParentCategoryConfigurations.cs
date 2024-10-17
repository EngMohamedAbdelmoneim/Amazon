using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Amazon.Core.Entities;
namespace Amazon.Core.Configurations
{
	public class ParentCategoryConfigurations
    {
        public void Configure(EntityTypeBuilder<ParentCategory> builder){
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
