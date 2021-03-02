using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IMealsImagesRepository : IRepository<MealImage>
	{
		public MealImage GetMealImage(int mealId);
	}
}
