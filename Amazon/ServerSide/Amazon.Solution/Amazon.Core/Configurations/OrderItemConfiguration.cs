using Amazon.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amazon.Core.Configurations
{
	internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.OwnsOne(orderItem => orderItem.Product, product => product.WithOwner());

			builder.Property(orderItem => orderItem.Price).HasColumnType("decimal(18,2)");
		}
	}
}
