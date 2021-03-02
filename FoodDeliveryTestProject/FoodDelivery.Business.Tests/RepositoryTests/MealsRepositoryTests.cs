using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Tests.Services;
using FoodDelivery.Client.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FoodDelivery.Business.Tests.RepositoryTests
{
	public class MealsRepositoryTests : GenericRepositoryTests<Meal, IMealsRepository>
	{
		private IAccountsRepository accountsRepository;
		private IRestaurantsRepository restaurantsRepository;
		private IMealsImagesRepository mealsImagesRepository;
		public MealsRepositoryTests(TestFixture<Startup> fixture) : base(fixture)
		{
			accountsRepository = (IAccountsRepository)fixture.Server.Host.Services.GetService(typeof(IAccountsRepository));
			restaurantsRepository = (IRestaurantsRepository)fixture.Server.Host.Services.GetService(typeof(IRestaurantsRepository));
			mealsImagesRepository = (IMealsImagesRepository)fixture.Server.Host.Services.GetService(typeof(IMealsImagesRepository));
		}


		[Fact]
		public async Task AddEntityTest()
		{
			//Arrange
			var meal = await GetNewMeal();
			var newMeal = new Meal();
			try
			{
				//Act
				newMeal = await repository.AddAsync(meal);

				//Assert
				Assert.Equal(newMeal.Name, meal.Name);
				Assert.Equal(newMeal.Description, meal.Description);
				Assert.Equal(newMeal.Image.Image, meal.Image.Image);
				Assert.Equal(newMeal.Price, meal.Price);
				Assert.NotEqual(newMeal.EntityId, meal.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(newMeal);
			}
		}

		[Fact]
		public async Task UpdateEntityTest()
		{
			//Arrange
			var meal = await GetNewAddedEntity();
			meal.Name = "Another Name";
			try
			{
				//Act
				var updatedMeal = repository.Update(meal);

				//Assert
				Assert.Equal(updatedMeal.Name, meal.Name);
				Assert.Equal(updatedMeal.Description, meal.Description);
				Assert.Equal(updatedMeal.Image.Image, meal.Image.Image);
				Assert.Equal(updatedMeal.Price, meal.Price);
				Assert.Equal(updatedMeal.RestaurantId, meal.RestaurantId);
			}
			finally
			{
				//Clean
				CleaningDb(meal);
			}
		}

		[Fact]
		public async Task CascadeRemoveAccount()
		{
			//Arrange
			Meal meal = await GetNewAddedEntity();

			//Act
			accountsRepository.Remove(meal.Restaurant.OwnerId);
			var foundEntity = repository.Find(meal.EntityId);

			//Assert
			Assert.Null(foundEntity);
		}
		[Fact]
		public async Task CascadeRemoveRestaurant()
		{
			//Arrange
			Meal meal = await GetNewAddedEntity();
			var owner = meal.Restaurant.Owner;
			try
			{
				//Act
				restaurantsRepository.Remove(meal.Restaurant);
				var foundEntity = repository.Find(meal.EntityId);

				//Assert
				Assert.Null(foundEntity);
			}
			finally
			{
				//Clean
				accountsRepository.Remove(owner.AccountId);
			}
		}

		[Fact]
		public async Task GetMealsImageTest()
		{
			//Arrange
			var meal = await GetNewAddedEntity();
			try
			{
				//Act
				var image = mealsImagesRepository.GetImageForMeal(meal.EntityId);

				//Assert
				Assert.NotNull(image);
				Assert.False(image.Length == 0);
			}
			finally
			{
				//Clean
				CleaningDb(meal);
			}
		}


		[Fact]
		public async Task GetAllMealsForRestaurantTest()
		{
			//Arrange
			var meal = await GetNewAddedEntity();
			var secondMeal = EntitiesCreationService.GetMeal();
			secondMeal.RestaurantId = meal.RestaurantId;
			var seccondAddedMeal = await repository.AddAsync(secondMeal);

			try
			{
				//Act
				var meals = repository.GetAllMealsForRestaurant(meal.RestaurantId);

				//Assert
				Assert.Contains(meals, m => m.EntityId == meal.EntityId);
				Assert.Contains(meals, m => m.EntityId == seccondAddedMeal.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(meal);
			}
		}


		protected override async Task<Meal> GetNewAddedEntity()
		{
			var meal = await GetNewMeal();
			return await repository.AddAsync(meal);
		}

		private async Task<Meal> GetNewMeal()
		{
			var restaurant = await GetRestaurant();
			var meal = EntitiesCreationService.GetMeal();
			meal.RestaurantId = restaurant.RestaurantId;
			return meal;
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

		protected override void CleaningDb(Meal entity)
		{
			try
			{
				accountsRepository.Remove(entity.Restaurant.Owner);
			}
			catch { }

			try
			{
				restaurantsRepository.Remove(entity.RestaurantId);
			}
			catch { }

			try
			{
				repository.Remove(entity);
			}
			catch { }
		}
	}
}
