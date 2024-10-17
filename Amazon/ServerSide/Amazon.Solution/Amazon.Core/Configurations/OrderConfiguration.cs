using Amazon.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amazon.Core.Configurations
{
	internal class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(o => o.OrderStatus).HasConversion(
			OStatus => OStatus.ToString(),
			OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
			
			builder.Property(o => o.PaymentStatus).HasConversion(
			PStatus => PStatus.ToString(),
			PStatus => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), PStatus));

			builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
			
			builder.HasOne(o => o.PaymentMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
		}
	}
}
