using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDelivery.Business.Entities;

namespace FoodDelivery.DataAccess
{
	public class MealConfiguration : IEntityTypeConfiguration<Meal>
	{
		public void Configure(EntityTypeBuilder<Meal> builder)
		{
			builder.HasKey(meal => meal.MealId);
			builder.Ignore(meal => meal.EntityId);

			builder.Property(meal => meal.Price)
				.HasColumnType("decimal(10, 4)");

			builder.Property(meal => meal.Description)
				.HasMaxLength(255);

			builder.Property(meal => meal.Name)
				.HasMaxLength(50);

			builder.HasOne(meal => meal.Restaurant)
				.WithMany(restaurant => restaurant.Meals)
				.HasForeignKey(meal => meal.RestaurantId);

		}
	}
}
