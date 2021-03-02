using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDelivery.Business.Entities;

namespace FoodDelivery.DataAccess
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{

		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(order => order.OrderId);
			builder.Ignore(order => order.EntityId);

			builder.Property(order => order.Price)
				.HasColumnType("decimal(14, 4)");

			builder.HasOne(order => order.Customer)
				.WithMany(customer => customer.Orders)
				.HasForeignKey(order => order.CustomerId);
		}
	}
}
