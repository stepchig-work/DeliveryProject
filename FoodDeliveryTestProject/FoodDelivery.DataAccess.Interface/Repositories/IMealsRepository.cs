using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IMealsRepository : IRepository<Meal>
	{
		public List<Meal> GetAllMealsForRestaurant(int restaurantId);

		public Meal GetMealWithImage(int mealId);
	}
}
