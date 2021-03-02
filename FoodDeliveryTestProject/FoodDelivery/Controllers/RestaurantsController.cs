using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Business.Interface.Services;
using FoodDelivery.Client.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace FoodDelivery.Client.AngularWeb.Controllers
{
	[Route("api/[controller]")]
	public class RestaurantsController : Controller
	{
		IRestaurantsRepository restaurantsRepository;
		IRestaurantsImagesRepository restaurantsImagesRepository;
		IAccountsService accountService;
		IAccountsRepository accountsRepository;
		private readonly string noRightsForModifying = "No rights to add or modify this restaurant";
		private readonly string noRightsForViewing = "No rights to view this restaurant";

		public RestaurantsController(IRestaurantsRepository restaurantsRepository,
			IRestaurantsImagesRepository restaurantsImagesRepository,
			IAccountsService accountService,
			IAccountsRepository accountsRepository)
		{
			Contract.Requires(restaurantsRepository != null);
			Contract.Requires(restaurantsImagesRepository != null);
			Contract.Requires(accountService != null);
			Contract.Requires(accountsRepository != null);

			this.restaurantsRepository = restaurantsRepository;
			this.restaurantsImagesRepository = restaurantsImagesRepository;
			this.accountService = accountService;
			this.accountsRepository = accountsRepository;

		}

		[HttpPost("[action]")]
		public async Task<IActionResult> AddRestaurant([FromBody] RestaurantAddUpdateModel restaurantAddUpdateModel)
		{
			try
			{
				if (!IsRestaurantModifyingAllow(restaurantAddUpdateModel.AccountId, restaurantAddUpdateModel.Restaurant))
				{
					return BadRequest(noRightsForModifying);
				}
				var addedRestaurant = await restaurantsRepository.AddAsync(restaurantAddUpdateModel.Restaurant);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantAddUpdateModel restaurantAddUpdateModel)
		{
			try
			{
				if (!IsRestaurantModifyingAllow(restaurantAddUpdateModel.AccountId, restaurantAddUpdateModel.Restaurant))
				{
					return BadRequest(noRightsForModifying);
				}

				await UpdateRestaurantInRepository(restaurantAddUpdateModel);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("[action]")]
		public IActionResult GetImage([FromQuery] int restaurantId)
		{
			var image = restaurantsImagesRepository.GetImageForRestaurant(restaurantId);
			return Ok(image);
		}

		[HttpGet("[action]")]
		public IActionResult GetAll()
		{
			var restaurants = restaurantsRepository.GetEntities();
			return Ok(restaurants);
		}

		[HttpGet("[action]")]
		public IActionResult GetRestaurant(int accountId, int restaurantId)
		{			
			if(IsUserAllowedToGet(accountId, restaurantId))
			{
				var restaurant = restaurantsRepository.Find(restaurantId);
				return Ok(restaurant);
			}
			return BadRequest(noRightsForViewing);
		}

		[HttpGet("[action]")]
		public IActionResult GetRestaurantName([FromQuery] int restaurantId)
		{
			var name = restaurantsRepository.GetRestaurantName(restaurantId);
			var result = new
			{
				name = name
			};
			return Ok(result);
		}

		[HttpGet("[action]")]
		public IActionResult GetAllRestaurantsForUser([FromQuery] int accountId)
		{
			var restaurants = accountService.GetAllAvailableRestaurantsForUser(accountId);
			return Ok(restaurants);
		}

		[HttpGet("[action]")]
		public IActionResult GetAllRestaurantsForOwner([FromQuery] int accountId)
		{
			var restaurants = restaurantsRepository.GetOwnersRestaurants(accountId);
			return Ok(restaurants);
		}

		[HttpDelete("[action]")]
		public IActionResult RemoveRestaurant(int accountId, int restaurantId)
		{
			if (!IsRestaurantModifyingAllow(accountId, restaurantId))
			{
				return BadRequest(noRightsForModifying);
			}
			restaurantsRepository.Remove(restaurantId);
			return Ok();
		}
		private bool IsUserAllowedToGet(int accountId, int restaurantId)
		{
			return accountService.IsUserAllowedForRestaureant(accountId, restaurantId);
		}

		private bool IsRestaurantModifyingAllow(int accountId, Restaurant restaurant)
		{
			var account = accountsRepository.Find(accountId);
			return account != null && restaurant != null && account.AccountId == restaurant.OwnerId;

		}
		private bool IsRestaurantModifyingAllow(int accountId, int restaurantId)
		{
			var restaurant = restaurantsRepository.Find(restaurantId);
			return IsRestaurantModifyingAllow(accountId, restaurant);
		}

		private async Task UpdateRestaurantInRepository(RestaurantAddUpdateModel restaurantAddUpdateModel)
		{
			var restaurantImage = restaurantsImagesRepository.GetRestaurantImage(restaurantAddUpdateModel.Restaurant.EntityId);
			restaurantAddUpdateModel.Restaurant.Image.RestaurantId = restaurantAddUpdateModel.Restaurant.RestaurantId;
			restaurantsImagesRepository.Remove(restaurantImage);
			restaurantImage = restaurantAddUpdateModel.Restaurant.Image;
			restaurantAddUpdateModel.Restaurant.Image = null;
			restaurantsRepository.Update(restaurantAddUpdateModel.Restaurant);
			await restaurantsImagesRepository.AddAsync(restaurantImage);
		}
	}
}
