using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;

namespace FoodDelivery.DataAccess
{
	public class AccountValidator : BaseValidator<Account>, IAccountValidator
	{
		public AccountValidator()
		{
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(account => account.Name).NotEmpty();
			RuleFor(account => account.Surname).NotEmpty();
			RuleFor(account => account.UserName).NotEmpty();
			RuleFor(account => account.HashedPassword).NotEmpty();
		}
	}
}
