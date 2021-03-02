using FoodDelivery.Client.Entities;
using FoodDelivery.Business.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FoodDelivery.Business.Tests.Services;

namespace FoodDelivery.Business.Tests.RepositoryTests
{
	public class RestaurantsRepositoryTests : GenericRepositoryTests<Restaurant, IRestaurantsRepository>
	{
		private IAccountsRepository accountsRepository;
		private IRestaurantsImagesRepository restaurantImageRepository;
		public RestaurantsRepositoryTests(TestFixture<Startup> fixture) : base(fixture)
		{
			accountsRepository = (IAccountsRepository)fixture.Server.Host.Services.GetService(typeof(IAccountsRepository));
			restaurantImageRepository = (IRestaurantsImagesRepository)fixture.Server.Host.Services.GetService(typeof(IRestaurantsImagesRepository));
		}


		[Fact]
		public async Task AddEntityTest()
		{
			//Arrange
			var newRestaurant = await GetNewRestaurnt();
			var restaurant = new Restaurant();
			try
			{
				//Act
				restaurant = await repository.AddAsync(newRestaurant);

				//Assert
				Assert.Equal(restaurant.Name, newRestaurant.Name);
				Assert.Equal(restaurant.Description, newRestaurant.Description);
				Assert.Equal(restaurant.Image.Image, newRestaurant.Image.Image);
				Assert.NotEqual(restaurant.EntityId, newRestaurant.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(restaurant);
			}
		}

		[Fact]
		public async Task UpdateEntityTest()
		{
			//Arrange
			var restaurant = await GetNewAddedEntity();
			restaurant.Name = "NewName";
			try
			{
				//Act
				var updatedRestaurant = repository.Update(restaurant);

				//Assert
				Assert.Equal(restaurant.Name, updatedRestaurant.Name);
				Assert.Equal(restaurant.Description, updatedRestaurant.Description);
				Assert.Equal(restaurant.Image.Image, updatedRestaurant.Image.Image);
				Assert.Equal(restaurant.EntityId, updatedRestaurant.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(restaurant);
			}
		}

		[Fact]
		public async Task CascadeRemove()
		{
			//Arrange
			var restaurant = await GetNewAddedEntity();

			//Act
			accountsRepository.Remove(restaurant.OwnerId);
			var foundEntity = repository.Find(restaurant.EntityId);

			//Assert
			Assert.Null(foundEntity);
		}

		[Fact]
		public async Task GetRestaurantImageTest()
		{
			//Arrange
			var restaurant = await GetNewAddedEntity();
			try
			{
				//Act
				var image = restaurantImageRepository.GetImageForRestaurant(restaurant.RestaurantId);

				//Assert
				Assert.NotNull(image);
				Assert.False(image.Length == 0);
			}
			finally
			{
				//Clean
				CleaningDb(restaurant);
			}
		}
		[Fact]
		public async Task GetOwnersRestaurantsTest()
		{
			//Arrange
			var restaurant = await GetNewAddedEntity();
			var secondRestaurant = EntitiesCreationService.GetRestaurant();
			secondRestaurant.OwnerId = restaurant.OwnerId;
			var addedSecondRestaurant = await repository.AddAsync(secondRestaurant);
			try
			{
				//Act
				var restaurants = repository.GetOwnersRestaurants(restaurant.OwnerId);

				//Assert
				Assert.Contains(restaurants, r => r.RestaurantId == restaurant.RestaurantId);
				Assert.Contains(restaurants, r => r.RestaurantId == addedSecondRestaurant.RestaurantId);
			}
			finally
			{
				//Clean
				CleaningDb(restaurant);
				CleaningDb(secondRestaurant);
			}
		}
		

		protected override async Task<Restaurant> GetNewAddedEntity()
		{
			var restaurant = await GetNewRestaurnt();
			return await repository.AddAsync(restaurant);
		}

		private async Task<Restaurant> GetNewRestaurnt()
		{
			var restaurantImage = Properties.Resources.restaurantTestImage;
			var owner = await GetOwner();
			return new Restaurant()
			{
				Name = "SomeRestaurant",
				Description = "Some Restaurant Description",
				Image = new RestaurantImage
				{
					Image = restaurantImage
				},
				OwnerId = owner.AccountId				
			};
		}

		private async Task<Account> GetOwner()
		{
			var owner = EntitiesCreationService.GetOwner();
			return await accountsRepository.AddAccount(owner,"password");
		}

		protected override void CleaningDb(Restaurant restaurant)
		{
			try
			{
				accountsRepository.Remove(restaurant.OwnerId);
			}
			catch { }

			try
			{
				repository.Remove(restaurant);
			}
			catch { }
		}

		public override void Dispose()
		{
			base.Dispose();
		}
	}
}
