using FoodDelivery.Business.Interface.Services;
using System.Diagnostics.Contracts;
using Xunit;

namespace FoodDelivery.Business.Tests.ServicesTests
{
	public class AuthenticationServiceTests : IClassFixture<TestFixture<Startup>>
	{
		private readonly IAuthenticationService authenticationService;
		public AuthenticationServiceTests(TestFixture<Startup> fixture)
		{
			Contract.Requires(fixture != null);

			authenticationService = (IAuthenticationService)fixture.Server.Host.Services.GetService(typeof(IAuthenticationService));			
		}

		[Fact]
		public void TestEqualPasswords()
		{
			//Arrange
			var password = "SomePassword";

			//Act
			var hashedPassword = authenticationService.HashPassword(password);

			//Assert
			Assert.True(authenticationService.ComparePasswords(password, hashedPassword));
		}
		[Fact]
		public void TestDifferentPasswords()
		{
			//Arrange
			var firstPassword = "SomePassword";
			var secondPassword = "SomeOtherPassword";

			//Act
			var hashedPassword = authenticationService.HashPassword(firstPassword);

			//Assert
			Assert.False(authenticationService.ComparePasswords(secondPassword, hashedPassword));
		}
	}
}
