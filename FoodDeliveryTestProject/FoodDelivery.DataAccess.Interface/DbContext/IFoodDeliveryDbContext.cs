using FoodDelivery.Business.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodDelivery.DataAccess
{
	public interface IFoodDeliveryDbContext : IDisposable
	{
		public DbSet<Account> AccountsSet { get; set; }
		public DbSet<Order> OrdersSet { get; set; }
		public DbSet<Restaurant> RestaurantsSet { get; set; }
		public DbSet<Meal> MealsSet { get; set; }
		public DbSet<OrderStatus> OrderStatusesSet { get; set; }
		public DbSet<MealForOrder> MealsForOrdersSet { get; set; }
		public DbSet<MealImage> MealsImagesSet { get; set; }
		public DbSet<BlockedUsers> BlockedUsersSet { get; set; }
		public DbSet<RestaurantImage> RestaurantsImagesSet { get; set; }
	}

	
}
