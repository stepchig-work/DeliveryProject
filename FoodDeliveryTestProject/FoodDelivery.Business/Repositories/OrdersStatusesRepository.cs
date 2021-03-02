using AutoMapper;

using ClientOrderStatus = FoodDelivery.Client.Entities.OrderStatus;
using BusinessOrderStatus = FoodDelivery.Business.Entities.OrderStatus;

using ClientOrdersStatusRepository = FoodDelivery.Business.Interface.Repositories.IOrdersStatusRepository;
using BusinessOrdersStatusRepository = FoodDelivery.DataAccess.Interface.IOrdersStatusRepository;
using System.Collections.Generic;

namespace FoodDelivery.Business.Repositories
{
	public class OrdersStatusesRepository : GenericRepository<BusinessOrdersStatusRepository, ClientOrderStatus, BusinessOrderStatus>, ClientOrdersStatusRepository
	{
		public OrdersStatusesRepository(IMapper mapper, BusinessOrdersStatusRepository ordersStatusRepository)
			: base(mapper, ordersStatusRepository)
		{
		}

		public IEnumerable<ClientOrderStatus> GetAllStatusesForOrder(int orderId)
		{
			var orderStatuses = new List<ClientOrderStatus>();
			var recievedStatuses = innerRepository.GetAllStatusesForOrder(orderId);
			foreach(var status in recievedStatuses)
			{
				orderStatuses.Add(mapper.Map<ClientOrderStatus>(status));
			}
			return orderStatuses;
		}
	}
}
