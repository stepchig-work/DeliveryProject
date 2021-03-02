using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System;

namespace FoodDelivery.DataAccess
{
	public class BlockedUserValidator : BaseValidator<BlockedUsers>, IBlockedUserValidator
	{
		public BlockedUserValidator()
		{
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(blockedUser => blockedUser.AccountId).NotNull().NotEmpty();
		}
	}
}
