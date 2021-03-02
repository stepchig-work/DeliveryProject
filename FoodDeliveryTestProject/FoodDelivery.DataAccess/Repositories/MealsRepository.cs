using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.DataAccess
{
	public class MealsRepository : BaseRepository<Meal, FoodDeliveryDbContext>, IMealsRepository
	{
		public MealsRepository(IMealValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{

		}

		public List<Meal> GetAllMealsForRestaurant(int restaurantId) => dbContext.MealsSet.Where(meal => meal.RestaurantId == restaurantId).ToList();

		public Meal GetMealWithImage(int mealId) => dbContext.MealsSet
			.Include(r => r.Image)
			.FirstOrDefault(r => r.RestaurantId == mealId);

		protected override Meal FindById(int id, FoodDeliveryDbContext dbContext) => dbContext.MealsSet.FirstOrDefault(meal => meal.MealId == id);
	}
}
