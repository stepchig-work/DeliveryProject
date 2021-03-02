using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IRestaurantsImagesRepository : IRepository<RestaurantImage>
	{
		public RestaurantImage GetRestaurantImage(int restaurantId);
	}
}
