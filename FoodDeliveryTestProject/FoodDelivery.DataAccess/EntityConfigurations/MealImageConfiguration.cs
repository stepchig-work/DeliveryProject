using FoodDelivery.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.DataAccess.EntityConfigurations
{
	public class MealImageConfiguration : IEntityTypeConfiguration<MealImage>
	{
		public void Configure(EntityTypeBuilder<MealImage> builder)
		{
			builder.HasKey(mealImage => mealImage.MealImageId);
			builder.Ignore(mealImage => mealImage.EntityId);

			builder.Property(mealImage => mealImage.Image).HasColumnType("image");

			builder.HasOne(mealImage => mealImage.Meal)
				.WithOne(meal => meal.Image)
				.HasForeignKey<MealImage>(mealImage => mealImage.MealId);
		}
	}
}
