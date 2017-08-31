using GameStore.Common.Entities;

namespace GameStore.Authentification.Abstract
{
	public interface IApiAuthentication
	{
		string LogIn(string login, string password, bool isPersistent);

		User GetUserBy(string token);

		User CurrentUser { get; }
	}
}