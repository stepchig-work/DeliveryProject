using System.Threading.Tasks;
using System.Linq;
using Xunit;
using FoodDelivery.Business.Interface.Repositories;
using FoodDelivery.Client.Entities;
using FoodDelivery.Business.Interface.Services;
using System.Collections.Generic;

namespace FoodDelivery.Business.Tests.RepositoryTests
{
	public class AccountsRepositoryTests
	: GenericRepositoryTests<Account, IAccountsRepository>
	{
		public readonly string accountsPassword = "RandomPassword";
		public readonly IAuthenticationService authenticationService;

		public AccountsRepositoryTests(TestFixture<Startup> fixture) : base(fixture)
		{
			authenticationService = (IAuthenticationService)fixture.Server.Host.Services.GetService(typeof(IAuthenticationService));
		}

		[Fact]
		public async Task AddAccountTest()
		{
			//Arrange
			var newAccount = GetNewAccount();
			//Act
			var account = await repository.AddAccount(newAccount, "RandomPassword");

			//Assert
			Assert.Equal(newAccount.Name, account.Name);
			Assert.Equal(newAccount.Surname, account.Surname);
			Assert.Equal(newAccount.Address, account.Address);
			Assert.Equal(newAccount.Role, account.Role);
			Assert.Equal(newAccount.UserName, account.UserName);
			Assert.NotEqual(newAccount.EntityId, account.EntityId);

			//Clean
			CleaningDb(account);
		}
		[Fact]
		public async Task AddAccountPasswordTest()
		{
			//Arrange
			var newAccount = await GetNewAddedEntity();
			//Act
			var newAccountPassword = repository.GetAccountHashedPassword(newAccount.UserName);

			//Assert
			Assert.True(authenticationService.ComparePasswords(accountsPassword, newAccountPassword));

			//Clean
			CleaningDb(newAccount);
		}
		[Fact]
		public async Task UpdateAccountTest()
		{
			//Arrange
			Account account = await GetNewAddedEntity();
			account.Name = "Another Name";

			//Act
			var updatedAccount = repository.Update(account);

			//Assert
			Assert.Equal(account.Name, updatedAccount.Name);
			Assert.Equal(account.Surname, updatedAccount.Surname);
			Assert.Equal(account.Address, updatedAccount.Address);
			Assert.Equal(account.Role, updatedAccount.Role);
			Assert.Equal(account.UserName, updatedAccount.UserName);
			Assert.Equal(account.EntityId, updatedAccount.EntityId);

			//Clean
			CleaningDb(account);
		}

		[Fact]
		public async Task GetAccountByUsernameTest()
		{
			//Arrange
			Account account = await GetNewAddedEntity();
			try
			{

				//Act
				var accountFound = repository.GetAccountByUsername(account.UserName);

				//Assert
				Assert.Equal(account.EntityId, accountFound.EntityId);
			}
			finally
			{
				//Clean
				CleaningDb(account);
			}
		}
		[Fact]
		public async Task GetAccountByUsernameNegativeTest()
		{
			//Arrange
			Account account = await GetNewAddedEntity();
			try
			{

				//Act
				var accountFound = repository.GetAccountByUsername(account.UserName + "wrong");

				//Assert
				Assert.Null(accountFound);
			}
			finally
			{
				//Clean
				CleaningDb(account);
			}
		}
		[Fact]
		public async Task IsAccountExistsNegativeTest()
		{
			//Arrange
			Account account = await GetNewAddedEntity();
			try
			{
				//Act
				var result = repository.IsAccountExists(account.UserName + "wrong");

				//Assert
				Assert.False(result);
			}
			finally
			{
				//Clean
				CleaningDb(account);
			}
		}
		[Fact]
		public async Task IsAccountExistsTest()
		{
			//Arrange
			Account account = await GetNewAddedEntity();
			try
			{
				//Act
				var result = repository.IsAccountExists(account.UserName);

				//Assert
				Assert.True(result);
			}
			finally
			{
				//Clean
				CleaningDb(account);
			}
		}

		protected override async Task<Account> GetNewAddedEntity()
		{
			return await repository.AddAccount(GetNewAccount(), accountsPassword);
		}

		private  Account GetNewAccount()
		{
			return new Account()
			{
				Name = "Stefan",
				Surname = "Stefanion",
				Address = "Some Street",
				Role = AccountRoles.RegularUser,
				UserName = "stefan3000",
				Restaurants = new List<Restaurant>(),
				Orders = new List<Order>()
			};
		}

		protected override void CleaningDb(Account entity)
		{
			try
			{
				repository.Remove(entity);
			}
			catch { }
			
		}

	}

}