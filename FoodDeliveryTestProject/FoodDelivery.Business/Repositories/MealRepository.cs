using AutoMapper;

using ClientMeal = FoodDelivery.Client.Entities.Meal;
using BusinessMeal = FoodDelivery.Business.Entities.Meal;

using ClientMealsRepository = FoodDelivery.Business.Interface.Repositories.IMealsRepository;
using BusinessMealsRepository = FoodDelivery.DataAccess.Interface.IMealsRepository;
using System.Collections.Generic;

namespace FoodDelivery.Business.Repositories
{
	public class MealRepository : GenericRepository<BusinessMealsRepository, ClientMeal, BusinessMeal>, ClientMealsRepository
	{
		public MealRepository(IMapper mapper, BusinessMealsRepository mealsRepository)
			:base(mapper, mealsRepository)
		{
		}

		public List<ClientMeal> GetAllMealsForRestaurant(int restaurantId)
		{
			var meals = new List<ClientMeal>();
			var searchedMeals = innerRepository.GetAllMealsForRestaurant(restaurantId);
			foreach(var searchedMeal in searchedMeals)
			{
				meals.Add(mapper.Map<ClientMeal>(searchedMeal));
			}
			return meals;
		}

		public ClientMeal GetMealWithImage(int mealId) => mapper.Map<ClientMeal>(innerRepository.GetMealWithImage(mealId));
	}
}
