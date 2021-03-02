using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDelivery.Business.Entities;

namespace FoodDelivery.DataAccess
{
	public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>

	{
		public void Configure(EntityTypeBuilder<OrderStatus> builder)
		{
			builder.HasKey(orderStatus => orderStatus.OrderStatusId);
			builder.Ignore(orderStatus => orderStatus.EntityId);

			builder.HasOne(orderStatus => orderStatus.Order)
				.WithMany(order => order.OrderStatuses)
				.HasForeignKey(orderStatus => orderStatus.OrderId);
		}
	}
}
