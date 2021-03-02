using Microsoft.Extensions.DependencyInjection;

using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Repositories;
using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Business.Services;

namespace FoodDelivery.Common.ServiceCollectionExtentions
{
	public static class BusinessServiceColletionExtention
	{
		public static void ConfigureBusinessProject(this IServiceCollection services)
		{
			ConfigureRepositories(services);
			ConfigureServices(services);
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IAccountsService, AccountsService>();
		}

		private static void ConfigureRepositories(IServiceCollection services)
		{
			services.AddScoped<IAccountsRepository, AccountsRepositoty>();
			services.AddScoped<IMealsRepository, MealRepository>();
			services.AddScoped<IMealsForOrdersRepository, MealForOrderRepository>();
			services.AddScoped<IMealsImagesRepository, MealsImagesRepository>();
			services.AddScoped<IOrdersRepository, OrdersRepository>();
			services.AddScoped<IOrdersStatusRepository, OrdersStatusesRepository>();
			services.AddScoped<IRestaurantsRepository, RestaurantRepository>();
			services.AddScoped<IRestaurantsImagesRepository, RestaurntsImagesRepository>();
			services.AddScoped<IBlockedUsersRepository, BlockedUsersRepository>();
		}
	}
}
