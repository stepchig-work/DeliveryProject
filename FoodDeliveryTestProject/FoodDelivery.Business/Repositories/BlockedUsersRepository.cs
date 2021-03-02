using AutoMapper;

using ClientBlockedUsers = FoodDelivery.Client.Entities.BlockedUsers;
using BusinessBlockedUsers = FoodDelivery.Business.Entities.BlockedUsers;

using ClientBlockedUsersRepository = FoodDelivery.Business.Interface.Repositories.IBlockedUsersRepository;
using BusinessBlockedUsersRepository = FoodDelivery.DataAccess.Interface.IBlockedUsersRepository;
using FoodDelivery.Business.Interface.Services;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Business.Repositories
{
	public class BlockedUsersRepository : GenericRepository<BusinessBlockedUsersRepository, ClientBlockedUsers, BusinessBlockedUsers>, ClientBlockedUsersRepository
	{
		public BlockedUsersRepository(IMapper mapper, BusinessBlockedUsersRepository accountsRepository, IAuthenticationService authenticationService)
			: base(mapper, accountsRepository)
		{
		}

		public IEnumerable<ClientBlockedUsers> BlokedUserOwners(int accountId)
		{
			var blockedForResult = new List<ClientBlockedUsers>();
			var blockedFor = innerRepository.BlokedUserOwners(accountId).ToList();

			foreach(var blocked in blockedFor)
			{
				blockedForResult.Add(mapper.Map<ClientBlockedUsers>(blocked));
			}
			return blockedForResult;
		}
	}
}
