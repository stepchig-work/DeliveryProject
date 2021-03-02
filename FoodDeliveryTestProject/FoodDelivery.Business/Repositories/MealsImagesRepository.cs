using AutoMapper;

using ClientMealImage = FoodDelivery.Client.Entities.MealImage;
using BusinessMealImage = FoodDelivery.Business.Entities.MealImage;

using ClientMealsImagesRepository = FoodDelivery.Business.Interface.Repositories.IMealsImagesRepository;
using BusinessMealsImagesRepository = FoodDelivery.DataAccess.Interface.IMealsImagesRepository;

namespace FoodDelivery.Business.Repositories
{
	public class MealsImagesRepository : GenericRepository<BusinessMealsImagesRepository, ClientMealImage, BusinessMealImage>, ClientMealsImagesRepository
	{
		public MealsImagesRepository(IMapper mapper, BusinessMealsImagesRepository mealsImagesRepository)
		: base(mapper, mealsImagesRepository)
		{
		}

		public byte[] GetImageForMeal(int mealId) => innerRepository.GetMealImage(mealId).Image;
		public ClientMealImage GetMealImage(int mealId) => mapper.Map<ClientMealImage>(innerRepository.GetMealImage(mealId));

	}
}
