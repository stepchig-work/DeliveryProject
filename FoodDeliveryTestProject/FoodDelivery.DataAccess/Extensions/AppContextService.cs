
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace FoodDelivery.DataAccess
{
	public static class AppContextService
	{
		public static IServiceCollection AddAppContext(this IServiceCollection services, string connections)
		{
			services.AddDbContext<FoodDeliveryDbContext>(options =>
				options.UseSqlServer(connections));
			return services;
		}
	}
}
