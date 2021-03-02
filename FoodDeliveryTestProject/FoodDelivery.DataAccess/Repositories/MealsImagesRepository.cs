using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Linq;

namespace FoodDelivery.DataAccess
{
	public class MealsImagesRepository : BaseRepository<MealImage, FoodDeliveryDbContext>, IMealsImagesRepository
	{
		public MealsImagesRepository(IMealImageValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{
		}

		public MealImage GetMealImage(int mealId) => dbContext.MealsImagesSet.FirstOrDefault(mi => mi.MealId == mealId);

		protected override MealImage FindById(int id, FoodDeliveryDbContext dbContext) => dbContext.MealsImagesSet.FirstOrDefault(mi => mi.MealImageId == id);

	}
}
