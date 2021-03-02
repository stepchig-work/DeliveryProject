using FoodDelivery.Client.Entities;
using System.Threading.Tasks;

namespace FoodDelivery.Business.Interface.Services
{
	public interface IOrderService
	{
		public
		Task UpdateStaus(int accountId, int orderId, OrderStatuses newOrderStatus);
	}
}
