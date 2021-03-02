using AutoMapper;

using ClientRestaurantImage = FoodDelivery.Client.Entities.RestaurantImage;
using BusinessRestaurantImage = FoodDelivery.Business.Entities.RestaurantImage;

using ClientRestaurantsImagesRepository = FoodDelivery.Business.Interface.Repositories.IRestaurantsImagesRepository;
using BusinessRestaurantsImagesRepository = FoodDelivery.DataAccess.Interface.IRestaurantsImagesRepository;

namespace FoodDelivery.Business.Repositories
{
	public class RestaurntsImagesRepository : GenericRepository<BusinessRestaurantsImagesRepository, ClientRestaurantImage, BusinessRestaurantImage>, ClientRestaurantsImagesRepository
	{
		public RestaurntsImagesRepository(IMapper mapper, BusinessRestaurantsImagesRepository restaurantsImagesRepository)
			: base(mapper, restaurantsImagesRepository)
		{
		}

		public ClientRestaurantImage GetRestaurantImage(int restaurantId) => mapper.Map<ClientRestaurantImage>(innerRepository.GetRestaurantImage(restaurantId));
		public byte[] GetImageForRestaurant(int restaurantId) => innerRepository.GetRestaurantImage(restaurantId).Image;

	}
}
