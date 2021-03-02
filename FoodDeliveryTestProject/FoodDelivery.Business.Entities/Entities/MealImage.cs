using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Entities
{
	public class MealImage : IIdentifiableEntity
	{
		public int EntityId
		{
			get => MealImageId;
			set => MealImageId = value;
		}
		public int MealImageId { get; set; }
		public byte[] Image { get; set; }
		public Meal Meal { get; set; }
		public int MealId { get; set; }
	}
}
