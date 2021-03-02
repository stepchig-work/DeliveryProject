using AutoMapper;

using ClientRestaurant = FoodDelivery.Client.Entities.Restaurant;
using BusinessRestaurant = FoodDelivery.Business.Entities.Restaurant;

using ClientRestaurantsRepository = FoodDelivery.Business.Interface.Repositories.IRestaurantsRepository;
using BusinessRestaurantsRepository = FoodDelivery.DataAccess.Interface.IRestaurantsRepository;
using System.Collections.Generic;

namespace FoodDelivery.Business.Repositories
{
	public class RestaurantRepository : GenericRepository<BusinessRestaurantsRepository, ClientRestaurant, BusinessRestaurant>, ClientRestaurantsRepository
	{
		public RestaurantRepository(IMapper mapper, BusinessRestaurantsRepository restaurantsRepository)
			: base(mapper, restaurantsRepository)
		{
		}

		public List<ClientRestaurant> GetOwnersRestaurants(int ownerId)
		{
			var restaurants = innerRepository.GetOwnersRestaurants(ownerId);
			var mappedRestaurants = new List<ClientRestaurant>();
			foreach (var order in restaurants)
			{
				mappedRestaurants.Add(mapper.Map<ClientRestaurant>(order));
			}
			return mappedRestaurants;
		}

		public string GetRestaurantName(int restaurantId) => innerRepository.Find(restaurantId).Name;

		public ClientRestaurant GetRestaurantWithImage(int restaurantId) => mapper.Map<ClientRestaurant>(innerRepository.GetRestaurantWithImage(restaurantId));
	}
}
