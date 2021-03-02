using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IMealsRepository : IRepository<Meal>
	{
		public List<Meal> GetAllMealsForRestaurant(int restaurantId);

		public Meal GetMealWithImage(int mealId);
	}
}
