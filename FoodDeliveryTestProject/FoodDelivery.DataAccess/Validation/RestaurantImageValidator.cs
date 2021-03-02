using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;

namespace FoodDelivery.DataAccess
{
	public class RestaurantImageValidator : BaseValidator<RestaurantImage>, IRestaurantImageValidator
	{
		public RestaurantImageValidator()
		{
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(restaurantImage => restaurantImage.Image).NotNull();
		}
	}
}
