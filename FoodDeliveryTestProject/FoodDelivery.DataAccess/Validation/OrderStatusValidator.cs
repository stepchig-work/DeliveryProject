using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;

namespace FoodDelivery.DataAccess
{
	public class OrderStatusValidator : BaseValidator<OrderStatus>, IOrderStatusValidator
	{
		public OrderStatusValidator()
		{
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(orderStatus => orderStatus.Status).NotNull();
		}
	}
}
