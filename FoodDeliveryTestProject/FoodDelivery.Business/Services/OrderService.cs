using FoodDelivery.Client.Entities;
using FoodDelivery.Business.Interface.Services;
using System;
using System.Threading.Tasks;
using FoodDelivery.Business.Interface.Repositories;
using System.Diagnostics.Contracts;
using System.Linq;

namespace FoodDelivery.Business.Services
{
	public class OrderService : IOrderService
	{
		IOrdersStatusRepository ordersStatusRepository;
		IAccountsRepository accountsRepositoty;
		IClientCommunicationService clientCommunicationService;
		IRestaurantsRepository restaurantsRepository;
		IOrdersRepository ordersRepository;
		public OrderService(IOrdersRepository ordersRepository,
			IOrdersStatusRepository ordersStatusRepository,
			IAccountsRepository accountsRepositoty,
			IRestaurantsRepository restaurantsRepository,
			IClientCommunicationService clientCommunicationService)
		{
			Contract.Requires(ordersRepository != null);
			Contract.Requires(ordersStatusRepository != null);
			Contract.Requires(accountsRepositoty != null);
			Contract.Requires(restaurantsRepository != null);
			Contract.Requires(clientCommunicationService != null);

			this.ordersRepository = ordersRepository;
			this.ordersStatusRepository = ordersStatusRepository;
			this.accountsRepositoty = accountsRepositoty;
			this.restaurantsRepository = restaurantsRepository;
			this.clientCommunicationService = clientCommunicationService;
		}
		public async Task UpdateStaus(int accountId, int orderId, OrderStatuses newOrderStatus)
		{
			var account = accountsRepositoty.Find(accountId);

			if (account == null || !IsChangeAllowed(account.Role, newOrderStatus) || !IsNextStep(newOrderStatus, orderId))
			{
				throw new Exception("CantUpdate");
			}

			var newStatus = new OrderStatus()
			{
				StatusChangeTime = DateTime.Now,
				OrderId = orderId,
				Status = newOrderStatus
			};
			var order = ordersRepository.GetOrderWithStatusAndMeals(orderId);
			order.LatestOrderStatus = newOrderStatus;

			order.OrderStatuses.Add(newStatus);
			ordersRepository.Update(order);

			var restaurant = restaurantsRepository.Find(order.RestaurantId);
			await clientCommunicationService.OrderStatusChangedNotify((int)order.CustomerId, orderId, restaurant.OwnerId, newOrderStatus);
		}
		private bool IsNextStep(OrderStatuses newOrderStatus, int orderId)
		{
			var orderStatuses = ordersStatusRepository.GetAllStatusesForOrder(orderId);
			if (orderStatuses.Any(os => os.Status == OrderStatuses.Canceled))
			{
				return false;
			}
			var curStatus = orderStatuses.Max(os => os.Status);
			if (curStatus == OrderStatuses.Placed)
			{
				return newOrderStatus == OrderStatuses.Canceled || newOrderStatus == OrderStatuses.Processed;
			}
			return curStatus + 1 == newOrderStatus;
		}

		private bool IsChangeAllowed(AccountRoles role, OrderStatuses nextOrderStatus)
		{
			if (role == AccountRoles.RegularUser)
			{
				return nextOrderStatus == OrderStatuses.Canceled
					|| nextOrderStatus == OrderStatuses.Received;
			}
			return nextOrderStatus == OrderStatuses.Processed
				|| nextOrderStatus == OrderStatuses.InRoute
				|| nextOrderStatus == OrderStatuses.Delivered;
		}
	}

}
