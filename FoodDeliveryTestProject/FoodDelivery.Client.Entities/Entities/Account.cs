using System.Collections.Generic;
using FoodDelivery.Common.Interface;

namespace FoodDelivery.Client.Entities
{
	public class Account : IIdentifiableEntity
	{
		public int EntityId
		{
			get => AccountId;
			set => AccountId = value;
		}
		public int AccountId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Address { get; set; }
		public AccountRoles Role { get; set; }
		public string UserName { get; set; }
		public string HashedPassword { get; set; }
	}
}
