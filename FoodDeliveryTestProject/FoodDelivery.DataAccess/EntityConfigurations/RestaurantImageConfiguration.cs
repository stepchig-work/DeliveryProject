using FoodDelivery.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.DataAccess
{
	class RestaurantImageConfiguration : IEntityTypeConfiguration<RestaurantImage>
	{
		public void Configure(EntityTypeBuilder<RestaurantImage> builder)
		{
			builder.HasKey(restaurantImage => restaurantImage.RestaurantImageId);
			builder.Ignore(restaurantImage => restaurantImage.EntityId);

			builder.Property(restaurantImage => restaurantImage.Image).HasColumnType("image");

			builder.HasOne(restaurantImage => restaurantImage.Restaurant)
				.WithOne(restaurant => restaurant.Image)
				.HasForeignKey<RestaurantImage>(restaurantImage => restaurantImage.RestaurantId);
		}
	}
}
