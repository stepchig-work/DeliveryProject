using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Entities
{
	public class MealForOrder : IIdentifiableEntity
	{
		public int EntityId
		{
			get => MealForOrderId;
			set => MealForOrderId = value;
		}
		public int MealForOrderId { get; set; }
		public int AmmountOfMeals { get; set; }
		public string MealName { get; set; }
		public decimal MealPrice { get; set; }
		public Order Order { get; set; }
		public int OrderId { get; set; }
	}
}
