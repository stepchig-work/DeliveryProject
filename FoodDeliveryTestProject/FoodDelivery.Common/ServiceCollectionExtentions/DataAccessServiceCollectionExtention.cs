using FoodDelivery.DataAccess;
using FoodDelivery.DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Common.ServiceCollectionExtentions
{
	public static class DataAccessServiceCollectionExtention
	{
		public static void ConfigureDataAccessProject(this IServiceCollection services)
		{
			ConfigureValidationServices(services);
			ConfigureRepositories(services);
		}

		private static void ConfigureValidationServices(IServiceCollection services)
		{
			services.AddTransient<IAccountValidator, AccountValidator>();
			services.AddTransient<IMealForOrderValidator, MealForOrderValidator>();
			services.AddTransient<IMealImageValidator, MealImageValidator>();
			services.AddTransient<IMealValidator, MealValidator>();
			services.AddTransient<IOrderStatusValidator, OrderStatusValidator>();
			services.AddTransient<IOrderValidator, OrderValidator>();
			services.AddTransient<IRestaurantImageValidator, RestaurantImageValidator>();
			services.AddTransient<IRestaurantValidator, RestaurantValidator>();
			services.AddTransient<IBlockedUserValidator, BlockedUserValidator>();

		}
		private static void ConfigureRepositories(IServiceCollection services)
		{
			services.AddTransient<IAccountsRepository, AccountsRepository>();
			services.AddTransient<IMealsForOrdersRepository, MealsForOredersRepository>();
			services.AddTransient<IMealsImagesRepository, MealsImagesRepository>();
			services.AddTransient<IMealsRepository, MealsRepository>();
			services.AddTransient<IOrdersRepository, OrdersRepository>();
			services.AddTransient<IOrdersStatusRepository, OrdersStatusRepository>();
			services.AddTransient<IRestaurantsImagesRepository, RestaurantImageRepository>();
			services.AddTransient<IRestaurantsRepository, RestaurantsRepository>();
			services.AddTransient<IBlockedUsersRepository, BlockedUsersRepository>();
		}
	}
}
