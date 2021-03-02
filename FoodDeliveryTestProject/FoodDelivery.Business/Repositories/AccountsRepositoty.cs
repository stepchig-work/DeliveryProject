using AutoMapper;

using ClientAccount = FoodDelivery.Client.Entities.Account;
using BusinessAccount = FoodDelivery.Business.Entities.Account;

using ClientAccountRepository = FoodDelivery.Business.Interface.Repositories.IAccountsRepository;
using BusinessAccountRepository = FoodDelivery.DataAccess.Interface.IAccountsRepository;
using System.Threading.Tasks;
using FoodDelivery.Business.Interface.Services;

namespace FoodDelivery.Business.Repositories
{
	public class AccountsRepositoty : GenericRepository<BusinessAccountRepository, ClientAccount, BusinessAccount>, ClientAccountRepository
	{
		private IAuthenticationService authenticationService;

		public AccountsRepositoty(IMapper mapper, BusinessAccountRepository accountsRepository, IAuthenticationService authenticationService)
			:base(mapper, accountsRepository)
		{
			this.authenticationService = authenticationService;
		}

		public async Task<ClientAccount> AddAccount(ClientAccount account, string password)
		{
			var mappedAccount = mapper.Map<BusinessAccount>(account);
			mappedAccount.HashedPassword = authenticationService.HashPassword(password);
			var addedAccount = await innerRepository.AddAsync(mappedAccount);
			return mapper.Map<ClientAccount>(addedAccount);
		}

		public ClientAccount GetAccountByUsername(string username)
		{
			var account = innerRepository.GetAccountByUsername(username);
			return mapper.Map<ClientAccount>(account);
		}

		public string GetAccountHashedPassword(string username) => innerRepository.GetAccountByUsername(username).HashedPassword;

		public bool IsAccountExists(string username) => innerRepository.IsAccountExists(username);


	}
}
