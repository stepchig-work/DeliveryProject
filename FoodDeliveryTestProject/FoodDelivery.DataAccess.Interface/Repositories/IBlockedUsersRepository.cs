using FoodDelivery.Business.Entities;
using FoodDelivery.Common.Interface;
using System.Collections.Generic;

namespace FoodDelivery.DataAccess.Interface
{
	public interface IBlockedUsersRepository : IRepository<BlockedUsers>
	{

		public IEnumerable<BlockedUsers> BlokedUserOwners(int accountId);
	}
}
