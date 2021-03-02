using FoodDelivery.Client.Entities;
using System.Threading.Tasks;

namespace FoodDelivery.Business.Interface.Services
{
	public interface IClientCommunicationService
	{
		public Task OrderStatusChangedNotify(int regularUserId, int orderId, int ownerId, OrderStatuses orderStatus);
	}
}
