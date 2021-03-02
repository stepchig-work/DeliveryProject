using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;

namespace FoodDelivery.DataAccess
{
	public class MealImageValidator : BaseValidator<MealImage>, IMealImageValidator
	{
		public MealImageValidator()
		{
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(mealImage => mealImage.Image).NotNull();
		}
	}
}
