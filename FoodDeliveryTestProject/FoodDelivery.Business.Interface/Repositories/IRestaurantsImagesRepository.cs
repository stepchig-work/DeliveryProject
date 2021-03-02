using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IRestaurantsImagesRepository : IRepository<RestaurantImage>
	{
		public RestaurantImage GetRestaurantImage(int restaurantId);
		public byte[] GetImageForRestaurant(int restaurantId);
	}
}
