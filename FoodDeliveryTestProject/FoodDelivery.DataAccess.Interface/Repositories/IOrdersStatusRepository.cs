using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IOrdersStatusRepository : IRepository<OrderStatus>
	{
		public IEnumerable<OrderStatus> GetAllStatusesForOrder(int orderId);
	}
}
