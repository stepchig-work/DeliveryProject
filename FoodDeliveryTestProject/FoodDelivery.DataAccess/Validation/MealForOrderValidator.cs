using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System;

namespace FoodDelivery.DataAccess
{
	public class MealForOrderValidator : BaseValidator<MealForOrder>, IMealForOrderValidator
	{
		public MealForOrderValidator()
		{
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(mealForOrder => mealForOrder.AmmountOfMeals).GreaterThanOrEqualTo(1);
		}
	}
}
