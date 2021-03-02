﻿using FoodDelivery.Common.Interface;

namespace FoodDelivery.Business.Entities
{
	public class BlockedUsers : IIdentifiableEntity
	{
		public int EntityId
		{
			get => BlockedUserId;
			set => BlockedUserId = value;
		}
		public int BlockedUserId { get; set; }

		public int AccountId { get; set; }
		public  Account Owner { get; set; }
		public int OwnerId { get; set; }
	}
}
