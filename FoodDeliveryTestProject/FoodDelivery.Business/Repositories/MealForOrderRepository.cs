using AutoMapper;

using ClientMealForOrder = FoodDelivery.Client.Entities.MealForOrder;
using BusinessMealForOrder = FoodDelivery.Business.Entities.MealForOrder;

using ClientMealsForOrdersRepository = FoodDelivery.Business.Interface.Repositories.IMealsForOrdersRepository;
using BusinessMealsForOrdersRepository = FoodDelivery.DataAccess.Interface.IMealsForOrdersRepository;
using System.Collections.Generic;

namespace FoodDelivery.Business.Repositories
{
	public class MealForOrderRepository : GenericRepository<BusinessMealsForOrdersRepository, ClientMealForOrder, BusinessMealForOrder>, ClientMealsForOrdersRepository
	{
		public MealForOrderRepository(IMapper mapper, BusinessMealsForOrdersRepository mealsForOrdersRepository)
				: base(mapper, mealsForOrdersRepository)
		{
		}

		public List<ClientMealForOrder> GetMealsForOrder(int orderId)
		{
			var resultList = new List<ClientMealForOrder>();

			var meals = innerRepository.GetMealsForOrder(orderId);
			foreach(var meal in meals)
			{
				resultList.Add(mapper.Map<ClientMealForOrder>(meal));
			}
			return resultList;
		}
	}
}
