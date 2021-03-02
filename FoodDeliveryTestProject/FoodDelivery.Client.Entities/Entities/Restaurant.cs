using System.Collections.Generic;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.Client.Entities
{
	public class Restaurant : IIdentifiableEntity
	{
		public int EntityId
		{
			get => RestaurantId;
			set => RestaurantId = value;
		}
		public int RestaurantId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int OwnerId { get; set; }
		public RestaurantImage Image { get; set; }

	}
}
