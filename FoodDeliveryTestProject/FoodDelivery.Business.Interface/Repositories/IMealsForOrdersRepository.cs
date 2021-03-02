using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IMealsForOrdersRepository : IRepository<MealForOrder>
	{
		public List<MealForOrder> GetMealsForOrder(int orderId);
	}
}
