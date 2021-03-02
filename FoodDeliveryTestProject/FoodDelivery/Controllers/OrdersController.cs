using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Client.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Client.AngularWeb.Controllers
{
	[Route("api/[controller]")]
	public class OrdersController : Controller
	{
		private readonly string cantUpdateStatus = "Can't update status";

		IOrdersRepository ordersRepository;
		IOrdersStatusRepository ordersStatusRepository;
		IRestaurantsRepository restaurantsRepository;
		IOrderService orderService;
		IMealsForOrdersRepository mealsForOrdersRepository;

		public OrdersController(IOrdersRepository ordersRepository,
		IOrdersStatusRepository ordersStatusRepository,
		IRestaurantsRepository restaurantsRepository,
		IOrderService orderService,
		IMealsForOrdersRepository mealsForOrdersRepository)
		{
			Contract.Requires(ordersRepository != null);
			Contract.Requires(ordersStatusRepository != null);
			Contract.Requires(restaurantsRepository != null);
			Contract.Requires(orderService != null);
			Contract.Requires(mealsForOrdersRepository != null);

			this.ordersRepository = ordersRepository;
			this.ordersStatusRepository = ordersStatusRepository;
			this.restaurantsRepository = restaurantsRepository;
			this.orderService = orderService;
			this.mealsForOrdersRepository = mealsForOrdersRepository;
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> AddOrderAsync([FromBody] Order order)
		{
			try
			{
				order.OrderStatuses.Clear();
				order.OrderStatuses.Add(new OrderStatus
				{
					Status = OrderStatuses.Placed,
					StatusChangeTime = DateTime.Now
				});
				order.LatestOrderStatus = OrderStatuses.Placed;
				order.CreationDate = DateTime.Now;
				var addedOrder = await ordersRepository.AddAsync(order);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateStatusAsync([FromBody] OrderStatusChangeModel orderStatusChangeModel)
		{
			try
			{
				await orderService.UpdateStaus(orderStatusChangeModel.AccountId, orderStatusChangeModel.OrderId, orderStatusChangeModel.OrderStatus);
				return Ok();
			}
			catch
			{
				return BadRequest(cantUpdateStatus);
			}
		}

		[HttpGet("[action]")]
		public IActionResult GetAllOrdersForRegularUser([FromQuery] int accountId)
		{
			var oreders = ordersRepository.GetAllOrdersForRegularUser(accountId);
			return Ok(oreders);
		}

		[HttpGet("[action]")]
		public IActionResult GetAllOrdersForOwner([FromQuery] int accountId)
		{
			var orders = new List<Order>();
			var ownerRestaurants = restaurantsRepository.GetOwnersRestaurants(accountId);
			foreach(var restaurant in ownerRestaurants)
			{
				orders.AddRange(ordersRepository.GetAllOrdersForRestaurant(restaurant.EntityId));
			}
			return Ok(orders);
		}

		[HttpGet("[action]")]
		public IActionResult GetOrder([FromQuery] GetOrderModel getOrderModel)
		{


			var order = ordersRepository.Find(getOrderModel.OrderId);
			return Ok(order);
		}

		[HttpGet("[action]")]
		public IActionResult GetOrderStatuses([FromQuery] int orderId)
		{
			var orderStatuses = ordersStatusRepository.GetAllStatusesForOrder(orderId);
			return Ok(orderStatuses);
		}

		[HttpGet("[action]")]
		public IActionResult GetMealsForOrder([FromQuery] int orderId)
		{
			var orderMeals = mealsForOrdersRepository.GetMealsForOrder(orderId);
			return Ok(orderMeals);
		}
	}
}
