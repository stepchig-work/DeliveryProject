using FluentValidation;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Diagnostics.Contracts;

namespace FoodDelivery.DataAccess
{
	public class OrderValidator : BaseValidator<Order>, IOrderValidator
	{
		IOrderStatusValidator orderStatusValidator;
		public OrderValidator(IOrderStatusValidator orderStatusValidator)
		{
			Contract.Requires(orderStatusValidator != null);
			this.orderStatusValidator = orderStatusValidator;
			RegisterRules();
		}

		private void RegisterRules()
		{
			RuleFor(order => order.Price).GreaterThanOrEqualTo(0);
			RuleFor(order => order.RestaurantName).NotNull().NotEmpty();
			RuleForEach(order => order.OrderStatuses).SetValidator(orderStatusValidator);
		}
	}
}
