using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Client.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace FoodDelivery.Client.AngularWeb.SignalR
{
	public class SignalRCommunicationService : IClientCommunicationService
	{
		private readonly IHubContext<FoodDeliveryHub> hubContext;

		public SignalRCommunicationService(IHubContext<FoodDeliveryHub> hubContext)
		{
			Contract.Requires(hubContext != null);

			this.hubContext = hubContext;
		}
		public async Task OrderStatusChangedNotify(int regularUserId, int orderId, int ownerId, OrderStatuses orderStatus)
		{
			var args = new
			{
				regularUserId,
				orderId,
				ownerId,
				orderStatus
			};
			await hubContext.Clients.All.SendAsync("onOrderStatusChanged", args);
		}

	}
}

