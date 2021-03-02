using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.Client.Entities
{
	public class Meal : IIdentifiableEntity
	{
		public int EntityId
		{
			get => MealId;
			set => MealId = value;
		}
		public int MealId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int RestaurantId { get; set; }
		public MealImage Image { get; set; }
	}
}
