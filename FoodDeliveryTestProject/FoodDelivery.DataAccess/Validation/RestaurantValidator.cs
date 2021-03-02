using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Diagnostics.Contracts;

namespace FoodDelivery.DataAccess
{
	public class RestaurantValidator : BaseValidator<Restaurant>, IRestaurantValidator
	{
		IRestaurantImageValidator restaurantImageValidator;
		public RestaurantValidator(IRestaurantImageValidator restaurantImageValidator)
		{
			Contract.Requires(restaurantImageValidator != null);
			this.restaurantImageValidator = restaurantImageValidator;
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(restaurant => restaurant.Name).NotEmpty().NotNull();
			RuleFor(restaurant => restaurant.Image).SetValidator(restaurantImageValidator);
		}
	}
}
