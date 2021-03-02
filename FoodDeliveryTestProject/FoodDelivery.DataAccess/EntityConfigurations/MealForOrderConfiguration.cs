using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDelivery.Business.Entities;

namespace FoodDelivery.DataAccess
{
	public class MealForOrderConfiguration : IEntityTypeConfiguration<MealForOrder>
	{
		public void Configure(EntityTypeBuilder<MealForOrder> builder)
		{
			builder.HasKey(mealForOrder => mealForOrder.MealForOrderId);
			builder.Ignore(mealForOrder => mealForOrder.EntityId);

			builder.Property(mealForOrder => mealForOrder.MealPrice)
				.HasColumnType("decimal(10, 4)");

			builder.HasOne(mealForOrder => mealForOrder.Order)
				.WithMany(order => order.MealsForOrder)
				.HasForeignKey(mealForOrder => mealForOrder.OrderId);
		}
	}
}
