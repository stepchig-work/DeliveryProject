using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IOrdersRepository : IRepository<Order>
	{
		public List<Order> GetAllOrdersForRegularUser(int accountId);
		public List<Order> GetOrdersForRestaurant(int restaurantId);
		public Order GetOrderWithStatusAndMeals(int orderId);
	}
}
