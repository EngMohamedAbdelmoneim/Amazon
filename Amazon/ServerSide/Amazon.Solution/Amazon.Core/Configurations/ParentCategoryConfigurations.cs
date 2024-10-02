using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
