using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.DataAccess
{
	public class RestaurantsRepository : BaseRepository<Restaurant, FoodDeliveryDbContext>, IRestaurantsRepository
	{
		public RestaurantsRepository(IRestaurantValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{
		}

		public List<Restaurant> GetOwnersRestaurants(int ownerId) => dbContext.RestaurantsSet.Where(r => r.OwnerId == ownerId).ToList();

		public Restaurant GetRestaurantWithImage(int restaurantId) => dbContext.RestaurantsSet
			.Include(r => r.Image)
			.FirstOrDefault(r => r.RestaurantId == restaurantId);

		protected override Restaurant FindById(int id, FoodDeliveryDbContext dbContext) => dbContext.RestaurantsSet.FirstOrDefault(r => r.RestaurantId == id);

	}
}
