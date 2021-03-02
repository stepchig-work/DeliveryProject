using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IMealsForOrdersRepository : IRepository<MealForOrder>
	{
		public IEnumerable<MealForOrder> GetMealsForOrder(int orderId);
	}
}
