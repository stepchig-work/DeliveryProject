using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Business.Tests.Services;
using FoodDelivery.Client.Entities;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Xunit;

namespace FoodDelivery.Business.Tests.ServicesTests
{
	public class AccountServiceTests : IClassFixture<TestFixture<Startup>>
	{
		private readonly IAccountsRepository accountsRepository;
		private readonly IAccountsService accountsService;
		private readonly IBlockedUsersRepository blockedUsersRepository;
		public AccountServiceTests(TestFixture<Startup> fixture)
		{
			Contract.Requires(fixture != null);

			accountsRepository = (IAccountsRepository)fixture.Server.Host.Services.GetService(typeof(IAccountsRepository));
			accountsService = (IAccountsService)fixture.Server.Host.Services.GetService(typeof(IAccountsService));
			blockedUsersRepository = (IBlockedUsersRepository)fixture.Server.Host.Services.GetService(typeof(IBlockedUsersRepository));
		}

		[Fact]
		public async Task TestBlockUser()
		{
			//Arrange
			var owner = EntitiesCreationService.GetOwner("BlockerOwner");
			owner.HashedPassword = "password";
			var user = EntitiesCreationService.GetUser("BlockingUser");
			user.HashedPassword = "password";
			user = await accountsRepository.AddAsync(user);
			owner = await accountsRepository.AddAsync(owner);

			try
			{
				//Act
				await accountsService.BanUser(owner.AccountId, user.UserName);

				//Assert
				var blockedUserOwners = blockedUsersRepository.BlokedUserOwners(user.AccountId);
				Assert.Contains(blockedUserOwners, buo => buo.OwnerId == owner.AccountId && buo.AccountId == user.AccountId);
			}
			finally
			{
				//Clean
				accountsRepository.Remove(user);
				accountsRepository.Remove(owner);
			}
		}

		[Fact]
		public async Task TestGetAllAvailableRestaurantsForUser()
		{
			//Arrange
			var owner1 = EntitiesCreationService.GetOwner("BlockerOwner1");
			owner1.HashedPassword = "password";
			var restaurant1Owner1 = EntitiesCreationService.GetRestaurant();
			var restaurant2Owner1 = EntitiesCreationService.GetRestaurant();
			owner1.Restaurants = new List<Restaurant>() { restaurant1Owner1, restaurant2Owner1 };
			var owner2 = EntitiesCreationService.GetOwner("BlockerOwner2");
			owner2.HashedPassword = "password";
			var restaurant1Owner2 = EntitiesCreationService.GetRestaurant();
			var restaurant2Owner2 = EntitiesCreationService.GetRestaurant();
			owner2.Restaurants = new List<Restaurant>() { restaurant1Owner2, restaurant2Owner2 };

			var user1 = EntitiesCreationService.GetUser("BlockingUser1");
			user1.HashedPassword = "password";

			var user2 = EntitiesCreationService.GetUser("BlockingUser2");
			user2.HashedPassword = "password";

			user1 = await accountsRepository.AddAsync(user1);
			user2 = await accountsRepository.AddAsync(user2);
			owner1 = await accountsRepository.AddAsync(owner1);
			owner2 = await accountsRepository.AddAsync(owner2);

			await accountsService.BanUser(owner1.AccountId, user1.UserName);
			await accountsService.BanUser(owner2.AccountId, user2.UserName);
			try
			{
				//Act
				var firstRestaurants = accountsService.GetAllAvailableRestaurantsForUser(user1.AccountId);
				var secondRestaurants = accountsService.GetAllAvailableRestaurantsForUser(user2.AccountId);

				//Assert
				Assert.DoesNotContain(firstRestaurants,
					fr => fr.EntityId == owner1.Restaurants[0].EntityId
						|| fr.EntityId == owner1.Restaurants[1].EntityId);

				Assert.DoesNotContain(secondRestaurants,
					sr => sr.EntityId == owner2.Restaurants[0].EntityId
						|| sr.EntityId == owner2.Restaurants[1].EntityId);

				Assert.Contains(firstRestaurants,
					fr => fr.EntityId == owner2.Restaurants[0].EntityId
						|| fr.EntityId == owner2.Restaurants[1].EntityId);

				Assert.Contains(secondRestaurants,
					sr => sr.EntityId == owner1.Restaurants[0].EntityId
						|| sr.EntityId == owner1.Restaurants[1].EntityId);
			}
			finally
			{

				//Clean
				accountsRepository.Remove(user1);
				accountsRepository.Remove(owner1);
				accountsRepository.Remove(user2);
				accountsRepository.Remove(owner2);
			}
		}
		
	}
}
