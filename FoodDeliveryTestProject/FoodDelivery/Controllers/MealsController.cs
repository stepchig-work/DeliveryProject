using FoodDelivery.Business.Interface.Repositories;
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
	public class MealsController : Controller
	{
		IMealsRepository mealsRepository;
		IMealsImagesRepository mealsImagesRepository;
		IAccountsRepository accountsRepository;
		IRestaurantsRepository restaurantsRepository;

		private readonly string noRightsForModifying = "No rights to add or modify this meal";
		public MealsController(IMealsRepository mealsRepository,
			IMealsImagesRepository mealsImagesRepository,
			IAccountsRepository accountsRepository,
			IRestaurantsRepository restaurantsRepository)
		{
			Contract.Requires(mealsRepository != null);
			Contract.Requires(mealsImagesRepository != null);
			Contract.Requires(accountsRepository != null);
			Contract.Requires(restaurantsRepository != null);

			this.mealsRepository = mealsRepository;
			this.mealsImagesRepository = mealsImagesRepository;
			this.accountsRepository = accountsRepository;
			this.restaurantsRepository = restaurantsRepository;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> AddMeal([FromBody] MealAddUpdateModel mealAddUpdateModel)
		{
			try
			{
				if (!IsMealModifyingAllow(mealAddUpdateModel.AccountId, mealAddUpdateModel.Meal))
				{
					return BadRequest(noRightsForModifying);
				}
				var addedMeal = await mealsRepository.AddAsync(mealAddUpdateModel.Meal);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPut("[action]")]
		public async Task<IActionResult> UpdateMeal([FromBody] MealAddUpdateModel mealAddUpdateModel)
		{
			try
			{
				if (!IsMealModifyingAllow(mealAddUpdateModel.AccountId, mealAddUpdateModel.Meal))
				{
					return BadRequest(noRightsForModifying);
				}
				await UpdateMealInRepository(mealAddUpdateModel);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("[action]")]
		public IActionResult GetMeal([FromQuery] int mealId)
		{
			var image = mealsRepository.Find(mealId);
			return Ok(image);
		}

		[HttpGet("[action]")]
		public IActionResult GetImage([FromQuery] int mealId)
		{
			var image = mealsImagesRepository.GetImageForMeal(mealId);
			return Ok(image);
		}

		[HttpGet("[action]")]
		public IActionResult GetAllForRestaurant([FromQuery] int restaurantId)
		{
			var meals = mealsRepository.GetAllMealsForRestaurant(restaurantId);
			return Ok(meals);
		}

		[HttpGet("[action]")]
		public IActionResult GetAll()
		{
			var meals = mealsRepository.GetEntities();
			return Ok(meals);
		}


		[HttpDelete("[action]")]
		public IActionResult RemoveMeal([FromBody] RemoveMealModel removeMealModel)
		{

			if (!IsMealModifyingAllow(removeMealModel.AccountId, removeMealModel.MealId))
			{
				return BadRequest(noRightsForModifying);
			}
			mealsRepository.Remove(removeMealModel.MealId);
			return Ok();
		}


		private bool IsMealModifyingAllow(int accountId, Meal meal)
		{
			var account = accountsRepository.Find(accountId);
			var restaurant = restaurantsRepository.Find(meal.RestaurantId);
			return account != null && restaurant != null && account.AccountId == restaurant.OwnerId;
			
		}
		private bool IsMealModifyingAllow(int accountId, int mealId)
		{
			var meal = mealsRepository.Find(mealId);
			return IsMealModifyingAllow(accountId, meal);
		}

		private async Task UpdateMealInRepository(MealAddUpdateModel mealAddUpdateModel)
		{
			var mealImage = mealsImagesRepository.GetMealImage(mealAddUpdateModel.Meal.EntityId);
			mealAddUpdateModel.Meal.Image.MealId = mealAddUpdateModel.Meal.MealId;
			mealsImagesRepository.Remove(mealImage);
			mealImage = mealAddUpdateModel.Meal.Image;
			mealAddUpdateModel.Meal.Image = null;
			mealsRepository.Update(mealAddUpdateModel.Meal);
			await mealsImagesRepository.AddAsync(mealImage);
		}
	}
}
