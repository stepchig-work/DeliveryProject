using FoodDelivery.Client.Entities;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Business.Tests.Services
{
	public class EntitiesCreationService
	{
		public static Meal GetMeal()
		{
			var image = Properties.Resources.mealTestImage;
			return new Meal()
			{
				Name = "SomeMeal",
				Description = "Some description",
				Image = new MealImage()
				{
					Image = image
				},
				Price = 1
			};
		}
		public static Restaurant GetRestaurant()
		{
			var restaurantImage = Properties.Resources.restaurantTestImage;
			return new Restaurant()
			{
				Name = "SomeRestaurant",
				Description = "Some Restaurant Description",
				Image = new RestaurantImage
				{
					Image = restaurantImage
				},
			};
		}
		public static Order GetOrder()
		{
			return new Order()
			{
				CreationDate = DateTime.Now,
				Price = 0,
				RestaurantName ="someRestaurant",
				OrderStatuses = new List<OrderStatus>
				{
					new OrderStatus()
					{
						Status = OrderStatuses.Created,
						StatusChangeTime = DateTime.Now,
					}
				}
			};
		}
		public static Account GetOwner(string userName = "Owner")
		{
			return new Account()
			{
				Name = "Owner",
				Surname = "Ownerer",
				UserName = userName,
				Role = AccountRoles.RestaurantOwner
			};
		}
		public static Account GetUser(string userName = "User")
		{
			return new Account()
			{
				Name = "user",
				Surname = "Userer",
				UserName = userName,
				Role = AccountRoles.RegularUser
			};
		}
		public static OrderStatus GetOrderStatus()
		{
			return new OrderStatus()
			{
				Status = OrderStatuses.Created,
				StatusChangeTime = DateTime.Now
			};
		}
	}
}
