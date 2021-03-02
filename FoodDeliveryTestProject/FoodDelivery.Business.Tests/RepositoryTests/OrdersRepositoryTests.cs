using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Tests.Services;
using FoodDelivery.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodDelivery.Business.Tests.RepositoryTests
{
	public class OrdersRepositoryTests : GenericRepositoryTests<Order, IOrdersRepository>
	{
		private readonly IAccountsRepository accountsRepository;
		private readonly IRestaurantsRepository restaurantsRepository;
		public OrdersRepositoryTests(TestFixture<Startup> fixture) : base(fixture)
		{
			accountsRepository = (IAccountsRepository)fixture.Server.Host.Services.GetService(typeof(IAccountsRepository));
			restaurantsRepository = (IRestaurantsRepository)fixture.Server.Host.Services.GetService(typeof(IRestaurantsRepository));
		}

		[Fact]
		public async Task AddEntityTest()
		{
			//Arrange
			var newOrder = await GetNewOrder();
			var order = new Order();
			try
			{
				//Act
				order = await repository.AddAsync(newOrder);

				//Assert
				Assert.Equal(order.CreationDate, newOrder.CreationDate);
				Assert.Equal(order.CustomerId, newOrder.CustomerId);
				Assert.Equal(order.Price, newOrder.Price);
				Assert.NotEqual(order.EntityId, newOrder.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(order);
			}
		}
		[Fact]
		public async Task UpdateEntityTest()
		{
			//Arrange
			var newOrder = await GetNewAddedEntity();
			newOrder.Price = 5;
			try
			{
				//Act
				var updatedOrder = repository.Update(newOrder);

				//Assert
				Assert.Equal(updatedOrder.CreationDate, newOrder.CreationDate);
				Assert.Equal(updatedOrder.CustomerId, newOrder.CustomerId);
				Assert.Equal(updatedOrder.Price, newOrder.Price);
				Assert.Equal(updatedOrder.EntityId, newOrder.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(newOrder);
			}
		}
		[Fact]
		public async Task GetAllOrdersForRegularUserTest()
		{
			//Arrange
			var newOrder = await GetNewAddedEntity();
			var secondOrder = EntitiesCreationService.GetOrder();
			secondOrder.CustomerId = newOrder.CustomerId;
			secondOrder.RestaurantId = newOrder.RestaurantId;
			var addedSecondOrder = await repository.AddAsync(secondOrder);

			try
			{
				//Act
				var foundOrders = repository.GetAllOrdersForRegularUser((int)newOrder.CustomerId);

				//Assert
				Assert.Contains(foundOrders, fo => fo.EntityId == newOrder.EntityId);
				Assert.Contains(foundOrders, fo => fo.EntityId == addedSecondOrder.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(newOrder);
				CleaningDb(addedSecondOrder);
			}
		}
		[Fact]
		public async Task GetAllOrdersForRestaurantTest()
		{
			//Arrange
			var newOrder = await GetNewAddedEntity();
			var secondOrder = EntitiesCreationService.GetOrder();
			secondOrder.CustomerId = newOrder.CustomerId;
			secondOrder.RestaurantId = newOrder.RestaurantId;
			var addedSecondOrder = await repository.AddAsync(secondOrder);
			try
			{
				//Act
				var foundOrders = repository.GetAllOrdersForRestaurant(newOrder.RestaurantId);

				//Assert
				Assert.Contains(foundOrders, fo => fo.EntityId == newOrder.EntityId);
				Assert.Contains(foundOrders, fo => fo.EntityId == addedSecondOrder.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(newOrder);
				CleaningDb(addedSecondOrder);
			}
		}
		protected override void CleaningDb(Order entity)
		{
			try
			{
				accountsRepository.Remove(entity.Customer);
			}
			catch { }
			try
			{
				repository.Remove(entity);
			}
			catch { }
		}

		protected override async Task<Order> GetNewAddedEntity()
		{
			var order = await GetNewOrder();
			return await repository.AddAsync(order);
		}
		private async Task<Order> GetNewOrder()
		{
			var user = await GetUser();
			var restaurant = await GetRestaurant();
			var order = EntitiesCreationService.GetOrder();
			order.RestaurantId = restaurant.EntityId;
			order.CustomerId = user.EntityId;
			return order;
		}

		private async Task<Account> GetUser()
		{
			var user = EntitiesCreationService.GetUser();
			return await accountsRepository.AddAccount(user, "password");
		}

		private async Task<Restaurant> GetRestaurant()
		{
			var owner = await GetOwner();
			var restaurnt = EntitiesCreationService.GetRestaurant();
			restaurnt.OwnerId = owner.AccountId;

			return await restaurantsRepository.AddAsync(restaurnt);
		}
		private async Task<Account> GetOwner()
		{
			var owner = EntitiesCreationService.GetOwner();
			return await accountsRepository.AddAccount(owner, "password");
		}
	}
}
