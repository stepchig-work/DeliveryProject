using FoodDelivery.Client.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.Business.Interface.Repositories
{
	public interface IBlockedUsersRepository : IRepository<BlockedUsers>
	{
		public IEnumerable<BlockedUsers> BlokedUserOwners(int accountId);
	}
}
