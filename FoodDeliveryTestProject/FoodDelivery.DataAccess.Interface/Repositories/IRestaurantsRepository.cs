using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IRestaurantsRepository : IRepository<Restaurant>
	{
		public List<Restaurant> GetOwnersRestaurants(int ownerId);

		public Restaurant GetRestaurantWithImage(int restaurantId);
	}
}
