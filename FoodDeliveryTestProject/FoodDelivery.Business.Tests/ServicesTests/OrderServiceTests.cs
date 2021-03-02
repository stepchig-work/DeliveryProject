using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Business.Tests.Services;
using FoodDelivery.Client.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodDelivery.Business.Tests.ServicesTests
{
	public class OrderServiceTests : IClassFixture<TestFixture<Startup>>
	{
		IAccountsRepository accountsRepository;
		IRestaurantsRepository restaurantsRepository;
		IOrdersRepository ordersRepository;
		IOrdersStatusRepository ordersStatusRepository;
		IOrderService orderService;
		public OrderServiceTests(TestFixture<Startup> fixture)
		{
			Contract.Requires(fixture != null);

			accountsRepository = (IAccountsRepository)fixture.Server.Host.Services.GetService(typeof(IAccountsRepository));
			restaurantsRepository = (IRestaurantsRepository)fixture.Server.Host.Services.GetService(typeof(IRestaurantsRepository));
			ordersRepository = (IOrdersRepository)fixture.Server.Host.Services.GetService(typeof(IOrdersRepository));
			ordersStatusRepository = (IOrdersStatusRepository)fixture.Server.Host.Services.GetService(typeof(IOrdersStatusRepository));
			orderService = (IOrderService)fixture.Server.Host.Services.GetService(typeof(IOrderService));
		}

		[Theory]
		[InlineData(AccountRoles.RegularUser, OrderStatuses.Placed, OrderStatuses.Canceled)]
		[InlineData(AccountRoles.RegularUser, OrderStatuses.Delivered, OrderStatuses.Received)]
		[InlineData(AccountRoles.RestaurantOwner, OrderStatuses.Placed, OrderStatuses.Processed)]
		[InlineData(AccountRoles.RestaurantOwner, OrderStatuses.Processed, OrderStatuses.InRoute)]
		[InlineData(AccountRoles.RestaurantOwner, OrderStatuses.InRoute, OrderStatuses.Delivered)]
		public async Task UpdateStausTestPositive(AccountRoles accountRole, OrderStatuses previousOrderStatus, OrderStatuses nextStatus)
		{
			//Arrange
			var owner = EntitiesCreationService.GetOwner();
			owner.HashedPassword = "password";
			var restaurant = EntitiesCreationService.GetRestaurant();
			owner.Restaurants = new List<Restaurant>() { restaurant };
			owner = await accountsRepository.AddAsync(owner);

			
			var account = EntitiesCreationService.GetOwner();
			account.HashedPassword = "password";
			account.Role = accountRole;

			var order = EntitiesCreationService.GetOrder();
			
			var orderStatus = EntitiesCreationService.GetOrderStatus();
			orderStatus.Status = previousOrderStatus;
			order.OrderStatuses = new List<OrderStatus>() { orderStatus };
			order.RestaurantId = owner.Restaurants[0].RestaurantId;
			account.Orders = new List<Order>() { order };

			account = await accountsRepository.AddAsync(account);
			try
			{
				//Act
				await orderService.UpdateStaus(account.AccountId, account.Orders[0].EntityId, nextStatus);

				//Assert
				var statuses = ordersStatusRepository.GetAllStatusesForOrder(account.Orders[0].EntityId);
				Assert.Contains(statuses, s => s.Status == nextStatus);

				var orders = ordersRepository.GetAllOrdersForRegularUser(account.EntityId);
				Assert.Equal(orders[0].LatestOrderStatus, nextStatus);
			}
			finally
			{
				//Clear
				accountsRepository.Remove(account);
				accountsRepository.Remove(owner);
			}
		}

		[Theory]
		[InlineData(AccountRoles.RegularUser, OrderStatuses.Placed, OrderStatuses.Received)]
		[InlineData(AccountRoles.RegularUser, OrderStatuses.Placed, OrderStatuses.Processed)]
		[InlineData(AccountRoles.RestaurantOwner, OrderStatuses.Placed, OrderStatuses.InRoute)]
		[InlineData(AccountRoles.RestaurantOwner, OrderStatuses.InRoute, OrderStatuses.Processed)]
		[InlineData(AccountRoles.RestaurantOwner, OrderStatuses.Delivered, OrderStatuses.Received)]
		public async Task UpdateStausTestNegative(AccountRoles accountRole, OrderStatuses previousOrderStatus, OrderStatuses nextStatus)
		{
			//Arrange
			var owner = EntitiesCreationService.GetOwner();
			owner.HashedPassword = "password";
			var restaurant = EntitiesCreationService.GetRestaurant();
			owner.Restaurants = new List<Restaurant>() { restaurant };
			owner = await accountsRepository.AddAsync(owner);


			var account = EntitiesCreationService.GetOwner();
			account.HashedPassword = "password";
			account.Role = accountRole;

			var order = EntitiesCreationService.GetOrder();

			var orderStatus = EntitiesCreationService.GetOrderStatus();
			orderStatus.Status = previousOrderStatus;
			order.OrderStatuses = new List<OrderStatus>() { orderStatus };
			order.RestaurantId = owner.Restaurants[0].RestaurantId;
			account.Orders = new List<Order>() { order };

			account = await accountsRepository.AddAsync(account);
			try
			{
				//Act
				//Assert
				await Assert.ThrowsAnyAsync<Exception>(async () => await orderService.UpdateStaus(account.AccountId, account.Orders[0].EntityId, nextStatus));
			}
			finally
			{
				//Clear
				accountsRepository.Remove(account);
			}
		}
	}
}
