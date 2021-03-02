using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IRestaurantsRepository : IRepository<Restaurant>
	{
		public List<Restaurant> GetOwnersRestaurants(int ownerId);
		public string GetRestaurantName(int restaurantId);

		public Restaurant GetRestaurantWithImage(int restaurantId);
	}
}
