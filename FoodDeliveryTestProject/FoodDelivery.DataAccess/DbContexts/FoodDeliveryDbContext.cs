using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DataAccess
{
	public class FoodDeliveryDbContext : DbContext, IFoodDeliveryDbContext
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
		public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options){
		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AccountConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
			modelBuilder.ApplyConfiguration(new MealForOrderConfiguration());
			modelBuilder.ApplyConfiguration(new MealConfiguration());
			modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
			modelBuilder.ApplyConfiguration(new RestaurantImageConfiguration());
			modelBuilder.ApplyConfiguration(new MealImageConfiguration());
			modelBuilder.ApplyConfiguration(new BlockedUserConfiguration());
		}
	}
}
