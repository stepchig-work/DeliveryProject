using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IOrdersStatusRepository : IRepository<OrderStatus>
	{
		public IEnumerable<OrderStatus> GetAllStatusesForOrder(int orderId);
	}
}
