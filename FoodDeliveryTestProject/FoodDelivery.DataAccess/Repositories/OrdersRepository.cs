using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.DataAccess
{
	public class OrdersRepository : BaseRepository<Order, FoodDeliveryDbContext>, IOrdersRepository
	{
		public OrdersRepository(IOrderValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{

		}
		public List<Order> GetAllOrdersForRegularUser(int accountId) => dbContext.OrdersSet.Where(o => o.CustomerId == accountId).ToList();

		public List<Order> GetOrdersForRestaurant(int restaurantId) => dbContext.OrdersSet.Where(o => o.RestaurantId == restaurantId).ToList();


		protected override Order FindById(int id, FoodDeliveryDbContext dbContext) => dbContext.OrdersSet.FirstOrDefault(o => o.OrderId == id);
		
		public Order GetOrderWithStatusAndMeals(int orderId)
		{
			return dbContext.OrdersSet
				.Include(o => o.OrderStatuses)
				.Include(o => o.MealsForOrder)
				.FirstOrDefault(o => o.OrderId == orderId);
		}
	}
}
