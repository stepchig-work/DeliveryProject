using AutoMapper;

using ClientOrder = FoodDelivery.Client.Entities.Order;
using BusinessOrder = FoodDelivery.Business.Entities.Order;

using ClientOrdersRepository = FoodDelivery.Business.Interface.Repositories.IOrdersRepository;
using BusinessOrdersRepository = FoodDelivery.DataAccess.Interface.IOrdersRepository;
using System.Collections.Generic;

namespace FoodDelivery.Business.Repositories
{
	public class OrdersRepository : GenericRepository<BusinessOrdersRepository, ClientOrder, BusinessOrder>, ClientOrdersRepository
	{

		public OrdersRepository(IMapper mapper,	BusinessOrdersRepository ordersRepository)
			   : base(mapper, ordersRepository)
		{
		}

		public List<ClientOrder> GetAllOrdersForRegularUser(int accountId)
		{
			var orders = innerRepository.GetAllOrdersForRegularUser(accountId);
			var mappedOrders = new List<ClientOrder>();
			foreach (var order in orders)
			{
				mappedOrders.Add(mapper.Map<ClientOrder>(order));
			}
			return mappedOrders;
		}

		public List<ClientOrder> GetAllOrdersForRestaurant(int restaurantId)
		{
			var orders = innerRepository.GetOrdersForRestaurant(restaurantId);
			var mappedOrders = new List<ClientOrder>();
			foreach (var order in orders)
			{
				mappedOrders.Add(mapper.Map<ClientOrder>(order));
			}
			return mappedOrders;
		}

		public ClientOrder GetOrderWithStatusAndMeals(int orderId) => mapper.Map<ClientOrder>(innerRepository.GetOrderWithStatusAndMeals(orderId));
	}
}
