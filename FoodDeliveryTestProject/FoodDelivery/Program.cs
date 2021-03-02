using FoodDelivery.Client.AngularWeb.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FoodDelivery
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log4NetSetUp.SetUpLog4Net();

			CreateHostBuilder(args)
				.Build()
				.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
