using FoodDelivery.Common.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Client.Entities
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
		public int OrderId { get; set; }
		
	}
}
