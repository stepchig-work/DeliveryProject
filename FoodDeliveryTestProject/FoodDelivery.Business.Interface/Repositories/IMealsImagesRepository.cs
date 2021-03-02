using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IMealsImagesRepository : IRepository<MealImage>
	{
		public byte[] GetImageForMeal(int mealId);
		public MealImage GetMealImage(int mealId);
	}
}
