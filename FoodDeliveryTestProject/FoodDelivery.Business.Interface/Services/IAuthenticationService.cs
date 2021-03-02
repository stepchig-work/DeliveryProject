namespace FoodDelivery.Business.Interface.Services
{
	public interface IAuthenticationService
	{
		string HashPassword(string password);
		bool ComparePasswords(string checkPassword, string hashedPassword);
	}
}
