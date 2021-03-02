using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IOrdersRepository : IRepository<Order>
	{
		public List<Order> GetAllOrdersForRegularUser(int accountId);
		public List<Order> GetAllOrdersForRestaurant(int restaurantId);
		public Order GetOrderWithStatusAndMeals(int orderId);
	}
}
