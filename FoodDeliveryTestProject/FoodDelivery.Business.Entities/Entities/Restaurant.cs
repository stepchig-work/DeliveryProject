using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Entities
{
	public class Restaurant : IIdentifiableEntity
	{
		[NotMapped]
		public int EntityId
		{
			get => RestaurantId;
			set => RestaurantId = value;
		}
		public int RestaurantId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Account Owner { get; set; }
		public int OwnerId { get; set; }
		public RestaurantImage Image { get; set; }
		public List<Meal> Meals { get; set; }

	}
}
