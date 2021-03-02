using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Diagnostics.Contracts;

namespace FoodDelivery.DataAccess
{
	public class MealValidator : BaseValidator<Meal>, IMealValidator
	{
		IMealImageValidator mealImageValidator;
		public MealValidator(IMealImageValidator mealImageValidator)
		{
			Contract.Requires(mealImageValidator != null);

			this.mealImageValidator = mealImageValidator;
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(meal => meal.Name).NotEmpty();
			RuleFor(meal => meal.Price).GreaterThanOrEqualTo(0);
			RuleFor(meal => meal.Image).SetValidator(mealImageValidator);
		}
	}
}
