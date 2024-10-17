using Amazon.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amazon.Core.Configurations
{
	internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
	{
		public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			builder.Property(deliveryMethod => deliveryMethod.Cost).HasColumnType("decimal(18,2)");
		}
	}
}
