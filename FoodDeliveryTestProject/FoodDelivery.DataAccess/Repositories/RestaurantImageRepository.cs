using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Linq;

namespace FoodDelivery.DataAccess
{
	public class RestaurantImageRepository : BaseRepository<RestaurantImage, FoodDeliveryDbContext>, IRestaurantsImagesRepository
	{
		public RestaurantImageRepository(IRestaurantImageValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{
		}

		public RestaurantImage GetRestaurantImage(int restaurantId) => dbContext.RestaurantsImagesSet.FirstOrDefault(ri => ri.RestaurantId == restaurantId);


		protected override RestaurantImage FindById(int id, FoodDeliveryDbContext dbContext) => dbContext.RestaurantsImagesSet.FirstOrDefault(ri => ri.RestaurantImageId == id);

	}
}
