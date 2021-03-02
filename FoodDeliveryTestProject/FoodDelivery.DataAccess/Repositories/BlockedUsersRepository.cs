using AutoMapper;
using FoodDelivery.Business.Entities;
using FoodDelivery.DataAccess.Interface;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.DataAccess
{
	public class BlockedUsersRepository : BaseRepository<BlockedUsers, FoodDeliveryDbContext>, IBlockedUsersRepository
	{
		public BlockedUsersRepository(IBlockedUserValidator validator, IMapper mapper, FoodDeliveryDbContext dbContext) : base(dbContext, validator, mapper)
		{
		}

		public IEnumerable<BlockedUsers> BlokedUserOwners(int accountId)
		{
			return dbContext.BlockedUsersSet.Where(bu => bu.AccountId == accountId);
		}

		protected override BlockedUsers FindById(int id, FoodDeliveryDbContext dbContext)
		{
			return dbContext.BlockedUsersSet.FirstOrDefault(bu => bu.BlockedUserId == id);
		}
	}
}
