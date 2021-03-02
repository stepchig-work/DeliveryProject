using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDelivery.Business.Entities;

namespace FoodDelivery.DataAccess
{
	public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
	{

		public void Configure(EntityTypeBuilder<Restaurant> builder)
		{
			builder.HasKey(restaurant => restaurant.RestaurantId);
			builder.Ignore(restaurant => restaurant.EntityId);

			builder.HasOne(restaurant => restaurant.Owner)
				.WithMany(owner => owner.Restaurants)
				.HasForeignKey(restaurant => restaurant.OwnerId);

			builder.Property(restaurant => restaurant.Name)
				.HasMaxLength(50);

			builder.Property(restaurant => restaurant.Description)
				.HasMaxLength(255);
		}
	}
}
